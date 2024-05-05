using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

/// <summary>
/// This is the main server for the WiThrottleController. It manages incomming connections, tracks them in a
/// WiThrottleConnection and manages messages. It takes as parameters a RailwayConfig reference to allow it
/// to get information about the Entities (Locos, Turnouts etc) as well as a reference to the active DCC
/// controller so it can send messages to that controller.
///
/// Note that it does not need to track what messages it has sent as the LayoutConfig uses events to
/// track what commands have been sent and automatically update the Entities with state changes.
/// </summary>
/// <param name="railwayConfig"></param>The main layout collection of objects and settings
/// <param name="activeController"></param>An active IController instance
public class WiThrottleServer(IRailwayConfig railwayConfig, IController activeController) {

    private CancellationTokenSource cts = new CancellationTokenSource();
    private System.Timers.Timer   _heartbeatCheckTimer;
    private WiThrottlePreferences _preferences;

    public int ActiveClients { get; set; } = 0;

    /// <summary>
    ///     Start up the Listener Service using the provided Port and IPAddress
    /// </summary>
    public void Start() {

        // Load a set of preferences from a file but if it does not exit, just create
        // a default set (as normally we do not need to overwrite any of the settings)
        // ----------------------------------------------------------------------------
        _preferences = WiThrottlePreferences.Load() ?? new WiThrottlePreferences();

        // Make sure that the Service is not already running
        // -------------------------------------------------------
        if (ServiceHelper.IsServiceRunningOnPort(_preferences.Port)) {
            Logger.LogContext<WiThrottleServer>().Error("Service is already running. ");
            return;
        }

        // Start up the TCP Server to listen for incoming connections and process
        // ----------------------------------------------------------
        try {
            Logger.LogContext<WiThrottleServer>().Information("Starting the WiThrottle Server Listener.");
            var tcpServer = new TcpListener(_preferences.Address, _preferences.Port);
            tcpServer.Start();
            Task.Run(() => { StartListener(tcpServer); });
        }
        catch (Exception ex) {
            Logger.LogContext<WiThrottleServer>().Error("Server Crashed: {0}", ex);
            throw;
        }
    }

    /// <summary>
    ///     Causes the Server to stop listening and to shut down all threads that
    ///     have connections.
    /// </summary>
    public void Stop() {
        cts.Cancel();
    }

    private void StartListener(TcpListener server) {

        // Setup the Server to Broadcast its presence on the network
        // ----------------------------------------------------------
        ServerBroadcast.Start(_preferences);

        try {
            _heartbeatCheckTimer = new System.Timers.Timer(_preferences.HeartbeatCheckTime);
            _heartbeatCheckTimer.Elapsed += HeartbeatCheckHandler;
            _heartbeatCheckTimer.AutoReset = true;
            _heartbeatCheckTimer.Start();

            Logger.LogContext<WiThrottleServer>().Debug("Server Running: Waiting for a connection on {0}", server.LocalEndpoint);
            while (!cts.IsCancellationRequested) {
                var client = server.AcceptTcpClient();
                Thread t = new(HandleConnection);
                t.Start(client);
            }
            Logger.LogContext<WiThrottleServer>().Debug("Server Shutting Down on {0}", server.LocalEndpoint);
        }
        catch (SocketException e) {
            Logger.LogContext<WiThrottleServer>().Error("SocketException: {0}", e);
        }
        finally {
            _heartbeatCheckTimer.Stop();
            ForceCloseAllConnections();
            server.Stop();
            Logger.LogContext<WiThrottleServer>().Information("Server Stopped.");
        }
    }

    /// <summary>
    ///     Thread object to handle a request.
    /// </summary>
    /// <param name="obj"></param>
    private void HandleConnection(object? obj) {

        ActiveClients++;

        // This should not be possible but best to ensure and check for this edge case.
        // -----------------------------------------------------------------------------
        if (obj is not TcpClient client) {
            Logger.LogContext<WiThrottleServer>().Warning("Started thread but provided a NON-TCP Client Object.");
            return;
        }

        Logger.LogContext<WiThrottleServer>().Debug("Connection: Client '{0}' has connected.", client.Client.Handle);
        var stream = client.GetStream();
        var connection = _preferences.Connections.Add(client,_preferences,railwayConfig,activeController);
        connection.QueueMsg(new MsgConfiguration(connection));

        try {
            var bytesRead = 0;
            var bytes = new byte[256];
            StringBuilder buffer = new();

            while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) != 0) {
                // Read the data and append it to a String Builder in-case there is more data to read.
                // We are only reading 256 bytes maximum at a time from the stream so keep reading until
                // we get a terminator at the end of the data stream.
                // -------------------------------------------------------------------------------------
                var data = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                //Logger.LogContext<WiThrottleServer>().Debug($"CMD:RcvBuffer [{connection.ToString()}] => {Terminators.ForDisplay(data.Trim())}");
                buffer.Append(data);

                if (Terminators.HasTerminator(buffer)) {
                    foreach (var command in Terminators.GetMessagesAndLeaveIncomplete(buffer)) {
                        if (!string.IsNullOrEmpty(command)) {
                            if (WiThrottleCmdProcessor.Interpret(connection, command)) break;
                            SendServerMessages(connection, stream);
                        }
                    }
                    buffer.Clear();
                }
            }
            Logger.LogContext<WiThrottleServer>().Debug("Connection: Client '{0}' has closed.", connection?.ConnectionHandle);
        }
        catch (Exception e) {
            Logger.LogContext<WiThrottleServer>().Error("Exception: {0}", e);
        }
        finally {
            connection?.Close();
        }
        ActiveClients--;
    }

    /// <summary>
    /// While processing messages from the client we might need to generate one or more responses.
    /// Also, there may be an instance where changes need to be broadcast to the client and this allows
    /// us to inject messages to be sent.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="stream"></param>
    private void SendServerMessages(WiThrottleConnection connection, NetworkStream stream) {
        while (connection.HasMsg) {
            try {
                if (connection.NextMsg is { IsValid: true } message) {
                    var serverMessage = Encoding.ASCII.GetBytes(message.Message);
                    if (stream is { CanWrite: true }) {
                        Logger.Log.Information("{0}", message.ToString());
                        stream.Write(serverMessage, 0, serverMessage.Length);
                    }
                }
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to send message to the client : {0}", ex.Message);
                throw;
            }
        }
    }

    /// <summary>
    /// Heartbeat management - ensure that we get heartbeats from each connection
    /// and if we don't then force that connection to close but ensure we execute a
    /// stop all for any locos under that Throttles control.
    /// </summary>
    private void HeartbeatCheckHandler(object? sender, ElapsedEventArgs args) {
        //Logger.Log.Information($"Heartbeat Checker: {args.SignalTime}");
        CloseConnectionsWithCondition( connection => !connection.IsHeartbeatOk,
                                       "Did not get a Heartbeat from Client - terminating: {0}");
    }

    private void CloseConnectionsWithCondition(Func<WiThrottleConnection, bool> conditionToClose, string logMessage) {
        var connectionsToClose = _preferences.Connections.Where(conditionToClose).ToList();
        foreach (var connection in connectionsToClose) {
            Logger.Log.Information(logMessage, connection.ConnectionHandle);
            connection.Close();
        }
    }

    private void ForceCloseAllConnections() => CloseConnectionsWithCondition(connection => true, "Force Closing Connection: {0}");

}