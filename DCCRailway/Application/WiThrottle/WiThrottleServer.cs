using System.Net;
using System.Net.Sockets;
using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using Microsoft.AspNetCore.Http.Connections;
using ILogger = Serilog.ILogger;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleServer(WiThrottleServerOptions options) : IDisposable {

    private readonly ILogger log = Logger.LogContext<WiThrottleServer>();
    private readonly WiThrottleConnectionList _wiThrottleConnections = new();

    /// <summary>
    ///     Indicator that the server is currently active.
    ///     Set to FALSE to exit the while loop
    /// </summary>
    public bool ServerActive { get; set; } = false;

    /// <summary>
    ///     Start up the Listener Service using the provided Port and IPAddress
    /// </summary>
    public void Start() {

        // Make sure that the Service is not already running
        // -------------------------------------------------------
        if (ServiceHelper.IsServiceRunningOnPort(options.Port)) {
            log.Error("Service is already running. ");
            return;
        }

        // Setup the Server to Broadcast its presence on the network
        // ----------------------------------------------------------
        ServerBroadcast.Start(options);

        // Start up the TCP Server to listen for incoming connections and process
        // ----------------------------------------------------------
        try {
            log.Information("Starting the WiThrottle Server Listener.");
            using (var tcpServer = new TcpListener(options.Address, options.Port)) {
                tcpServer.Start();
                StartListener(tcpServer);
            }
        }
        catch (Exception ex) {
            log.Error("Server Crashed: {0}", ex);
            throw;
        }
        finally {
            Stop();
        }
    }

    /// <summary>
    ///     Causes the Server to stop listening and to shut down all threads that
    ///     have connections.
    /// </summary>
    public void Stop() {
        // Set the ServerActive flag to false which should terminate the Server
        ServerActive = false;
    }

    private void StartListener(TcpListener server) {
        ServerActive = true;
        try {
            log.Debug("Server Running: Waiting for a connection on {0}", server.LocalEndpoint);
            while (ServerActive) {
                var    client = server.AcceptTcpClient();
                Thread t      = new(HandleConnection);
                t.Start(client);
            }
            log.Debug("Server Shutting Down on {0}", server.LocalEndpoint);
            server.Stop();
        }
        catch (SocketException e) {
            log.Error("SocketException: {0}", e);
            server.Stop();
        }
    }

    /// <summary>
    ///     Thread object to handle a request.
    /// </summary>
    /// <param name="obj"></param>
    private void HandleConnection(object? obj) {

        // This should not be possible but best to ensure and check for this edge case.
        // -----------------------------------------------------------------------------
        if (obj is not TcpClient client) {
            log.Warning("Started thread but provided a NON-TCP Client Object.");
            return;
        }

        log.Debug("Connection: Client '{0}' has connected.", client.Client.Handle);
        var stream = client.GetStream();
        var connection = _wiThrottleConnections.Add((ulong)client.Client.Handle);
        var cmdFactory = new WiThrottleCmdFactory(connection, ref options);

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
                log.Debug($"{connection.ConnectionID:D4}: Received Data='{data.Trim()}'");
                buffer.Append(data);

                if (buffer.ToString().Contains(options.Terminator)) {
                    foreach (var command in buffer.ToString().Split(options.Terminator)) {
                        if (!string.IsNullOrEmpty(command)) {

                            // Get a command from the WiThrottle Handheld device
                            // Process that command and determine what to do with it
                            // The result will be a command or result set to return to the Throttle
                            // --------------------------------------------------------------------
                            var cmdToExecute = cmdFactory.Interpret(CommandType.Client, command);
                            log.Debug($"{connection.ConnectionID:D4}: Command to Execute='{cmdToExecute}'");

                            var msgToSend = cmdToExecute.Execute();
                            log.Debug($"{connection.ConnectionID:D4}: Response to send ='{msgToSend}'");
                            msgToSend.Execute(stream);

                            if (cmdToExecute is CmdQuit) {
                                _wiThrottleConnections.Delete(connection);
                                break;
                            }
                        }
                    }
                    buffer.Clear();
                }
                SendServerMessage(connection, stream);
            }
            log.Debug("Connection: Client '{0}' has closed.", connection?.ConnectionID);
        }
        catch (Exception e) {
            log.Error("Exception: {0}", e);
        }
    }

    /// <summary>
    /// While processing messages from the client we might need to generate one or more responses.
    /// Also, there may be an instance where changes need to be broadcast to the client and this allows
    /// us to inject messages to be sent.
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="stream"></param>
    private void SendServerMessage(WiThrottleConnection connection, NetworkStream stream) {
        while (connection.ServerMessages.HasMessages) {
            connection.ServerMessages.Next?.Execute(stream);
        }
    }

    public void AddBroadcastMessageToAll(IServerMsg message) {
        foreach (var connection in _wiThrottleConnections) {
            connection.ServerMessages.Add(message);
        }
    }

    public void Dispose() {
        Stop();
    }
}