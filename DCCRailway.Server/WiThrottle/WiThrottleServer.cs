using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DCCRailway.Server.Utilities;
using DCCRailway.Server.WiThrottle.Commands;

namespace DCCRailway.Server.WiThrottle {
	public class WiThrottleServer {
		private const ushort DEFAULT_PORT = 12090;
		public string terminator = "\x0A";
		public WiThrottleConnectionList wiThrottleConnections = new();

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

		public bool ServerActive { get; set; } = true;

		/// <summary>
		///     Start up the Listener Service using the provided Port and IPAddress
		/// </summary>
		/// <param name="ip">IPAddress to listen on</param>
		/// <param name="port">Port to Listen on</param>
		private void Start(IPAddress ipAddress, ushort port) {
			//
			// Setup the Server to Broadcast its presence on the network
			// ----------------------------------------------------------
			Dictionary<string, string> properties = new();
			properties.Add("node", "jmri-C4910CB13C68-3F39938d");
			properties.Add("jmri", "4.21.4");
			properties.Add("version", "4.2.1");
			ServerBroadcast.Start("JMRI WiThrottle Railway", "_withrottle._tcp", ipAddress, port);

			//
			// Start up the TCP Server to listen for incoming connections and process
			// ----------------------------------------------------------
			var TCPServer = new TcpListener(ipAddress, port);
			TCPServer.Start();
			StartListener(TCPServer);
		}

		/// <summary>
		///     Causes the Server to stop listening and to shut down all threads that
		///     have connections.
		/// </summary>
		public void Stop() {
			ServerActive = false;
		}

		public void StartListener(TcpListener server) {
			try {
				Console.WriteLine("Server Running: Waiting for a connection on {0}", server.LocalEndpoint);
				while (ServerActive) {
					var client = server.AcceptTcpClient();
					if (client != null) {
						Thread t = new(HandleConnection);
						t.Start(client);
					}
				}

				Console.WriteLine("Server Shutting Down on {0}", server.LocalEndpoint);
				server.Stop();
			} catch (SocketException e) {
				Console.WriteLine("SocketException: {0}", e);
				server.Stop();
			}
		}

		/// <summary>
		///     Thread object to handle a request.
		/// </summary>
		/// <param name="obj"></param>
		public void HandleConnection(object? obj) {
			// This should not be possible but best to ensure and check for this edge case.
			// -----------------------------------------------------------------------------
			if (obj == null || obj is not TcpClient) {
				Trace.TraceWarning("Started thread but provided a NULL or NON-TCP Client Object.");
				return;
			}

			var client = (TcpClient)obj;
			var stream = client.GetStream();
			Console.WriteLine("Connection: Client '{0}' has connected.", client.Client.Handle);
			var connectionEntry = wiThrottleConnections.Add((ulong)client.Client.Handle);
			var cmdFactory = new RecvCommandFactory(connectionEntry!);

			try {
				var bytesRead = 0;
				string? data = null;
				var bytes = new byte[256];
				StringBuilder buffer = new();

				while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) != 0) {
					// Read the data and append it to a String Builder incase there is more data to read.
					// We are only reading 256 bytes maximum at a time from the stream so keep reading until
					// we get a terminator at the end of the data stream.
					// -------------------------------------------------------------------------------------
					var hex = BitConverter.ToString(bytes);
					data = Encoding.ASCII.GetString(bytes, 0, bytesRead);
					Console.WriteLine($"{connectionEntry?.ConnectionID:D4}>>>>>{data.Trim()}");
					buffer.Append(data);

					if (buffer.ToString().Contains(terminator)) {
						foreach (var command in buffer.ToString().Split(terminator)) {
							if (!string.IsNullOrEmpty(command)) {
								var cmd = cmdFactory.Interpret(CommandType.Client, command!);
								if (cmd != null) {
									Console.WriteLine($"{connectionEntry?.ConnectionID:D4}<=={cmd?.ToString()}");
									var resData = cmd!.Execute();
									if (cmd is CmdQuit) {
										wiThrottleConnections.Disconnect(connectionEntry!);
										break;
									}

									if (resData != null && resData.Length > 0) {
										var reply = Encoding.ASCII.GetBytes(resData + terminator);
										stream.Write(reply, 0, reply.Length);
										Console.WriteLine($"{connectionEntry?.ConnectionID:D4}==>{resData}");
									}
								}
							}
						}

						buffer.Clear();
					}
				}

				Console.WriteLine("Connection: Client '{0}' has closed.", connectionEntry?.ConnectionID);
				client.Close();
			} catch (Exception e) {
				Console.WriteLine("Exception: {0}", e);
				client.Close();
			}
		}
	}
}