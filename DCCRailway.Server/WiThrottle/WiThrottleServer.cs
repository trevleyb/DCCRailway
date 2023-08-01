using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DCCRailway.System.Utilities;
using DCCRailway.Server.Utilities;
using DCCRailway.Server.WiThrottle.Commands;

namespace DCCRailway.Server.WiThrottle;

public class WiThrottleServer {
    private const ushort DEFAULT_PORT = 12090;
    private const string _terminator = "\x0A";
    private readonly WiThrottleConnectionList _wiThrottleConnections = new();

    /// <summary>
    ///     Default constructor which loads IP/Port from Config to
    ///     start the server.
    /// </summary>
    public WiThrottleServer() {
        Start(Network.GetLocalIPAddress(), DEFAULT_PORT);
    }

    /// <summary>
    ///     Constructor and start up of the Server listener
    /// </summary>
    /// <param name="ip">The IP that we will listen on (default to this IP) </param>
    /// <param name="port">The port that we will listen on</param>
    public WiThrottleServer(IPAddress ip, ushort port) {
        Start(ip, port);
    }

    /// <summary>
    ///     Indicator that the server is currently active.
    ///     Set to FALSE to exit the while loop
    /// </summary>
    private bool ServerActive { get; set; } = true;

    /// <summary>
    ///     Start up the Listener Service using the provided Port and IPAddress
    /// </summary>
    /// <param name="ip">IPAddress to listen on</param>
    /// <param name="port">Port to Listen on</param>
    private void Start(IPAddress ipAddress, ushort port) {
        //
        // Setup the Server to Broadcast its presence on the network
        // ----------------------------------------------------------
        // TODO: This needs to come via parameters as we start up multiple instances
        Dictionary<string, string> properties = new() { { "node", "jmri-C4910CB13C68-3F39938d" }, { "jmri", "4.21.4" }, { "version", "4.2.1" } };

        // TODO: The name should come from parameters 
        ServerBroadcast.Start("JMRI WiThrottle Railway", "_withrottle._tcp", ipAddress, port, properties);

        //
        // Start up the TCP Server to listen for incoming connections and process
        // ----------------------------------------------------------
        var tcpServer = new TcpListener(ipAddress, port);
        tcpServer.Start();
        StartListener(tcpServer);
    }

    /// <summary>
    ///     Causes the Server to stop listening and to shut down all threads that
    ///     have connections.
    /// </summary>
    public void Stop() {
        ServerActive = false;
    }

    private void StartListener(TcpListener server) {
        try {
            Logger.Log.Debug("Server Running: Waiting for a connection on {0}", server.LocalEndpoint);

            while (ServerActive) {
                var client = server.AcceptTcpClient();
                Thread t = new(HandleConnection);
                t.Start(client);
            }

            Logger.Log.Debug("Server Shutting Down on {0}", server.LocalEndpoint);
            server.Stop();
        }
        catch (SocketException e) {
            Logger.Log.Debug("SocketException: {0}", e);
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
            Logger.Log.Warning("Started thread but provided a NULL or NON-TCP Client Object.");

            return;
        }

        var stream = client.GetStream();
        Logger.Log.Debug("Connection: Client '{0}' has connected.", client.Client.Handle);
        var connectionEntry = _wiThrottleConnections.Add((ulong)client.Client.Handle);
        var cmdFactory = new WiThrottleCmdFactory(connectionEntry!);

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
                Logger.Log.Debug($"{connectionEntry?.ConnectionID:D4}>>>>>{data.Trim()}");
                buffer.Append(data);

                if (buffer.ToString().Contains(_terminator)) {
                    foreach (var command in buffer.ToString().Split(_terminator)) {
                        if (!string.IsNullOrEmpty(command)) {
                            var cmd = cmdFactory.Interpret(CommandType.Client, command!);
                            Logger.Log.Debug($"{connectionEntry?.ConnectionID:D4}<=={cmd}");

                            var resData = cmd!.Execute();

                            if (cmd is CmdQuit) {
                                _wiThrottleConnections.Disconnect(connectionEntry!);

                                break;
                            }

                            if (!string.IsNullOrEmpty(resData)) {
                                var reply = Encoding.ASCII.GetBytes(resData + _terminator);
                                stream.Write(reply, 0, reply.Length);
                                Logger.Log.Debug($"{connectionEntry?.ConnectionID:D4}==>{resData}");
                            }
                        }
                    }

                    buffer.Clear();
                }
            }

            Logger.Log.Debug("Connection: Client '{0}' has closed.", connectionEntry?.ConnectionID);
            client.Close();
        }
        catch (Exception e) {
            Logger.Log.Error("Exception: {0}", e);
            client.Close();
        }
    }
}