using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Controllers;
using DCCRailway.Layout;
using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.Messages;
using Serilog;
using Task = System.Threading.Tasks.Task;
using Timer = System.Timers.Timer;

namespace DCCRailway.WiThrottle;

/// <summary>
///     This is the main server for the WiThrottleController. It manages incomming connections, tracks them in a
///     WiThrottleConnection and manages messages. It takes as parameters a RailwayConfig reference to allow it
///     to get information about the Entities (Locos, Turnouts etc) as well as a reference to the active DCC
///     commandStation so it can send messages to that commandStation.
///     Note that it does not need to track what messages it has sent as the LayoutConfig uses events to
///     track what commands have been sent and automatically update the Entities with state changes.
/// </summary>

//public class Server(ILogger logger, IRailwaySettings railwaySettings, CommandStationManager cmdStationMgr) {
public class Server(ILogger logger, IRailwaySettings railwaySettings, StateManager.StateManager stateManager) {
    private readonly Connections             _connections = new(logger);
    private readonly CancellationTokenSource _cts         = new();
    private          ICommandStation         _commandStation;
    private          Timer                   _fastClockTimer;
    private          Timer                   _heartbeatCheckTimer;
    private          IPAddress               _hostAdress;
    private          int                     _hostPort;

    private int ActiveClients { get; set; } = 0;

    /// <summary>
    ///     Start up the Listener Service using the provided Port and IPAddress
    /// </summary>
    public void Start(ICommandStation commandStation) {
        _commandStation = commandStation;
        _hostAdress     = Network.GetLocalIpAddress();
        _hostPort       = railwaySettings.Settings.WiThrottle.HostPort;

        // Make sure that the Service is not already running
        // -------------------------------------------------------
        if (ServiceHelper.IsServiceRunningOnPort(_hostPort)) {
            logger.ForContext<Server>().Error("Service is already running. ");
            return;
        }

        // Start up the TCP Server to listen for incoming connections and process
        // ----------------------------------------------------------
        try {
            logger.Information("Starting the WiThrottle Server Listener on {0}:{1}.", _hostAdress, _hostPort);
            var tcpServer = new TcpListener(_hostAdress, _hostPort);
            tcpServer.Start();
            Task.Run(() => { StartListener(tcpServer); });
        } catch (Exception ex) {
            logger.Error("WiThrottle Server Ended Unexpectantly: {0}", ex.Message);
            throw;
        }
    }

    /// <summary>
    ///     Causes the Server to stop listening and to shut down all threads that
    ///     have connections.
    /// </summary>
    public void Stop() {
        logger.Information("Stopping the WiThrottle Server Listener on {0}:{1}.", _hostAdress, _hostPort);
        _cts.Cancel();
    }

    private void StartListener(TcpListener server) {
        // Setup the Server to Broadcast its presence on the network
        // ----------------------------------------------------------
        var serverBroadcaster = new ServerBroadcast();
        serverBroadcaster.Start(railwaySettings.Settings.WiThrottle);

        try {
            SetupHeartbeatTimer();
            SetupFastClockTimer();

            while (!_cts.IsCancellationRequested) {
                var    client = server.AcceptTcpClient();
                Thread t      = new(HandleConnectionAsync);
                t.Start(client);
            }

            logger.Information("WiThrottle Server Shutting Down on {0}", server.LocalEndpoint);
        } catch (SocketException e) {
            logger.Error("WiThrottle SocketException: {0}", e);
        } finally {
            _heartbeatCheckTimer.Stop();
            _fastClockTimer.Stop();
            ForceCloseAllConnections();
            serverBroadcaster.Stop();
            server.Stop();
            logger.Information("WiThrottle Server Stopped.");
        }
    }

    private void SetupFastClockTimer() {
        _heartbeatCheckTimer           =  new Timer(railwaySettings.Settings.WiThrottle.HeartbeatCheckTime * 1000);
        _heartbeatCheckTimer.Elapsed   += HeartbeatCheckHandler;
        _heartbeatCheckTimer.AutoReset =  true;
        _heartbeatCheckTimer.Start();
    }

    private void SetupHeartbeatTimer() {
        if (railwaySettings.WiThrottlePrefs.UseFastClock) {
            _fastClockTimer           =  new Timer(railwaySettings.Settings.WiThrottle.FastClockSeconds * 1000);
            _fastClockTimer.Elapsed   += FastClockTimerHandler;
            _fastClockTimer.AutoReset =  true;
            _fastClockTimer.Start();
        }
    }

    /// <summary>
    /// Re-Written Handle Connection Method which is non-blocking and Async.
    /// </summary>
    /// <param name="obj">Refernce to the TcpClient to manage</param>
    private async void HandleConnectionAsync(object? obj) {
        if (obj is not TcpClient client) {
            logger.Warning("WiThrottle Started thread but provided a NON-TCP Client Object.");
            return;
        }

        var clientHandle = client.Client?.Handle ?? 0;

        try {
            ActiveClients++;
            logger.Information("WiThrottle Connection: Client '{0}' on '{3}' has connected. [{1} active / {2} inactive connections]", clientHandle, ActiveClients, _connections.Count, client?.Client?.RemoteEndPoint?.ToString() ?? "UnknownIP");

            Debug.Assert(client != null, nameof(client) + " != null");
            var stream       = client.GetStream();
            var connection   = _connections.Add(client, railwaySettings, _commandStation);
            var cmdProcessor = new CommandProcessor(logger, _commandStation);

            connection.QueueMsg(new MsgConfiguration(connection));

            var           bytes  = new byte[256];
            StringBuilder buffer = new();

            while (!_cts.IsCancellationRequested) {
                var bytesRead = 0;

                try {
                    bytesRead = await stream.ReadAsync(bytes, 0, bytes.Length);
                } catch (ObjectDisposedException) {
                    break;
                }

                if (bytesRead != 0) {
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    if (IsBrowserRequest(data, connection, stream)) break;

                    buffer.Append(data);

                    if (Terminators.HasTerminator(buffer)) {
                        foreach (var command in Terminators.GetMessagesAndLeaveIncomplete(buffer))
                            if (!string.IsNullOrEmpty(command))
                                if (cmdProcessor.Interpret(connection, command))
                                    break;

                        buffer.Clear();
                    }
                }

                // Send any queued messages back to the Client
                // -----------------------------------------------------------
                if (connection.HasMsg) SendServerMessages(connection, stream);
            }

            connection?.Close();
        } catch (Exception ex) {
            logger.Error(ex, "WiThrottle Error: {0}", ex.Message);
        } finally {
            ActiveClients--;
        }

        logger.Information("WiThrottle Connection: Client '{0}' has closed. [{1} active / {2} inactive connections]", clientHandle, ActiveClients, _connections.Count);
    }

    private bool IsBrowserRequest(string data, Connection connection, NetworkStream stream) {
        if (data.StartsWith("GET")) {
            connection.ThrottleName = "Web Browser";
            SendServerMessages(BrowserMessage.Message(_connections, _hostAdress, _hostPort).ToString(), stream);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     While processing messages from the client we might need to generate one or more responses.
    ///     Also, there may be an instance where changes need to be broadcast to the client and this allows
    ///     us to inject messages to be sent.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="stream"></param>
    private void SendServerMessages(Connection connection, NetworkStream stream) {
        while (connection.HasMsg)
            try {
                // Get the message off the queue but only call the "Message" property
                // once as calling it can cause code to be executed such as getting the
                // power state, or a turnout state etc.
                // --------------------------------------------------------------------
                var messageStr = connection.NextMsg?.Message ?? "";

                if (!string.IsNullOrEmpty(Terminators.RemoveTerminators(messageStr)) && Terminators.HasTerminator(messageStr)) {
                    logger.Information("WiThrottle Sending Msg to [{0}]: {1}", connection.ConnectionHandle, Terminators.ForDisplay(messageStr));
                    SendServerMessages(messageStr, stream);
                }
            } catch (Exception ex) {
                logger.Error("WiThrottle Unable to send message to the client : {0}", ex.Message);
                throw;
            }
    }

    private void SendServerMessages(string serverMessage, NetworkStream stream) {
        SendServerMessages(Encoding.ASCII.GetBytes(serverMessage), stream);
    }

    private void SendServerMessages(byte[] serverMessage, NetworkStream stream) {
        try {
            if (stream is { CanWrite: true }) stream.Write(serverMessage, 0, serverMessage.Length);
        } catch (Exception ex) {
            logger.Error("WiThrottle Unable to send message to the client : {0}", ex.Message);
            throw;
        }
    }

    /// <summary>
    ///     Heartbeat management - ensure that we get heartbeats from each connection
    ///     and if we don't then force that connection to close but ensure we execute a
    ///     stop all for any locos under that Throttles control.
    /// </summary>
    private void HeartbeatCheckHandler(object? sender, ElapsedEventArgs args) {
        _connections.CloseConnectionsWithCondition(connection => !connection.IsHeartbeatOk, "WiThrottle Did not get a Heartbeat from Client - terminating: {0}");
    }

    private void FastClockTimerHandler(object? sender, ElapsedEventArgs e) {
        _connections.QueueMsgToAll(new MsgFastClock(railwaySettings));
    }

    private void ForceCloseAllConnections() {
        _connections.CloseConnectionsWithCondition(connection => true, "Force Closing Connection: {0}");
    }
}