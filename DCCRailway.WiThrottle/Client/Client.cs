using System.Net.Sockets;
using System.Text;
using System.Timers;
using DCCRailway.Common.Helpers;
using DCCRailway.WiThrottle.Client.Messages;
using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.ServiceHelper;
using Serilog;
using Serilog.Core;
using Timer = System.Timers.Timer;

namespace DCCRailway.WiThrottle.Client;

public class Client(ClientInfo clientInfo) {
    private TcpClient?     _client;
    private Timer          _heartbeatTimer;
    private ILogger        _logger = Logger.None;
    private bool           _running;
    private NetworkStream? _stream;

    public Client(string address, int port) : this(new ClientInfo(address, port)) { }
    public Client(string name, string address, int port) : this(new ClientInfo(name, address, port)) { }
    public Client(ServiceInfo service) : this(service.ClientInfo) { }

    public Turnouts Turnouts { get; } = new();

    public bool                      Echo { get; set; } = true;
    public event Action<IClientMsg>? MessageProcessed;
    public event Action<string>?     DataReceived;
    public event Action<string>?     ConnectionError;

    /// <summary>
    /// Connect to the WiThrottle Service via the given Address/Port
    /// </summary>
    /// <returns>A Result.OK if it connected or a Result.Fail if it could not. </returns>
    public IResult Connect() {
        try {
            if (_running) Stop();

            _client  = new TcpClient(clientInfo.Address, clientInfo.Port);
            _stream  = _client.GetStream();
            _running = true;

            var listenThread = new Thread(Listen);
            listenThread.Start();
            return Result.Ok();
        } catch (Exception ex) {
            return Result.Fail(ex);
        }
    }

    /// <summary>
    /// Listen for incomming messages and event them via the DataReceived event to the caller. 
    /// </summary>
    private void Listen() {
        StringBuilder buffer = new();
        var           bytes  = new byte[256];
        SendWakeUpMessages();

        while (_running) {
            try {
                if (_stream is { DataAvailable: true } && _client is { Connected: true }) {
                    var bytesRead = 0;
                    try {
                        bytesRead = _stream.Read(bytes, 0, bytes.Length);
                    } catch (ObjectDisposedException) {
                        break;
                    }

                    if (bytesRead != 0) {
                        var data = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        buffer.Append(data);

                        if (Terminators.HasTerminator(buffer)) {
                            foreach (var command in Terminators.GetMessagesAndLeaveIncomplete(buffer)) {
                                ProcessMessage(command);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                // Just ignore any exceptions for now, but this should raise events to say 
                // that we have an issue so that we can try to re-establish the connection
                ConnectionError?.Invoke(ex.Message);
            }

            Thread.Sleep(100);
        }
    }

    private void ProcessMessage(string message) {
        var clientMsg = new MessageProcessor(_logger, Turnouts).Interpret(message);
        switch (clientMsg) {
        case MsgQuit quit:
            _running = false;
            MessageProcessed?.Invoke(quit);
            break;
        case MsgHeartbeat heartbeat:
            StopHeartbeatTimer();
            StartHeartbeatTimer(heartbeat.HeartbeatSeconds);
            break;
        case MsgPanel panel:
            MessageProcessed?.Invoke(panel);
            break;
        default:
            MessageProcessed?.Invoke(clientMsg);
            DataReceived?.Invoke(message);
            break;
        }
    }

    private void SendWakeUpMessages() {
        // To initialise a connection to a WiThrottle service we need to send the following messages
        // ------------------------------------------------------------------------------------------
        SendMessage(clientInfo.GetNameMessage);
        SendMessage(clientInfo.GetHardwareMessage);
        SendMessage("*+");
    }

    private void SendShutdownMessages() {
        // To initialise a connection to a WiThrottle service we need to send the following messages
        // ------------------------------------------------------------------------------------------
        SendMessage("*-");
        SendMessage("Q");
    }

    /// <summary>
    /// Send a message to the Client
    /// </summary>
    /// <param name="message">The message, as a string, to send.</param>
    public void SendMessage(string message) {
        message = message.WithTerminator();
        try {
            if (_stream is { CanWrite: true }) {
                var data = Encoding.UTF8.GetBytes(message);
                _stream.Write(data, 0, data.Length);
                if (Echo) DataReceived?.Invoke(message);
            }
        } catch (Exception ex) {
            // Just ignore any exceptions for now, but this should raise events to say 
            // that we have an issue so that we can try to re-establish the connection
            ConnectionError?.Invoke(ex.Message);
        }
    }

    /// <summary>
    /// Shutdown the connection to the WiThrottle Service and clean up. 
    /// </summary>
    public void Stop() {
        if (_running) SendShutdownMessages();
        _running = false;
        if (_stream is { } stream) stream.Close();
        if (_client is { } client) client.Close();
    }

    private void StartHeartbeatTimer(int secs) {
        _heartbeatTimer           =  new Timer(secs * 1000);
        _heartbeatTimer.Elapsed   += HeartbeatTimerOnElapsed;
        _heartbeatTimer.AutoReset =  true;
        _heartbeatTimer.Start();
    }

    private void HeartbeatTimerOnElapsed(object? sender, ElapsedEventArgs e) {
        SendMessage("*");
    }

    private void StopHeartbeatTimer() {
        _heartbeatTimer.Elapsed   -= HeartbeatTimerOnElapsed;
        _heartbeatTimer.AutoReset =  false;
        _heartbeatTimer.Stop();
    }
}