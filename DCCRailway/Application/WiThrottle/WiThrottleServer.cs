using System.Net;
using System.Net.Sockets;
using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;
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
        var connectionEntry = _wiThrottleConnections.Add((ulong)client.Client.Handle);
        var cmdFactory      = new WiThrottleCmdFactory(connectionEntry!, ref options);

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
                log.Debug($"{connectionEntry?.ConnectionID:D4}>>>>>{data.Trim()}");
                buffer.Append(data);

                if (buffer.ToString().Contains(options.Terminator)) {
                    foreach (var command in buffer.ToString().Split(options.Terminator)) {
                        if (!string.IsNullOrEmpty(command)) {

                            // Get a command from the WiThrottle Handheld device
                            // Process that command and determine what to do with it
                            // The result will be a command or result set to return to the Throttle
                            // --------------------------------------------------------------------
                            var cmdToExecute = cmdFactory.Interpret(CommandType.Client, command!);
                            log.Debug($"{connectionEntry?.ConnectionID:D4}<=={cmdToExecute}");
                            var replyStr = cmdToExecute.Execute();
                            log.Debug($"{connectionEntry?.ConnectionID:D4}==>{replyStr}");

                            if (cmdToExecute is CmdQuit) {
                                if (connectionEntry != null) _wiThrottleConnections.Delete(connectionEntry);
                                break;
                            }

                            // If we have some data to reply with (returned from EXECUTE()) then
                            // send this data back to the Client.
                            // -----------------------------------------------------------------
                            if (!string.IsNullOrEmpty(replyStr)) {
                                var reply = Encoding.ASCII.GetBytes(replyStr + options.Terminator);
                                stream.Write(reply, 0, reply.Length);
                            }
                        }
                    }
                    buffer.Clear();
                }
            }
            log.Debug("Connection: Client '{0}' has closed.", connectionEntry?.ConnectionID);
        }
        catch (Exception e) {
            log.Error("Exception: {0}", e);
        }
    }

    public void Dispose() {
        Stop();
    }
}