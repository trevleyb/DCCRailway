using DCCPacketAnalyser.Analyser;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Tasks;
using Serilog;

namespace DCCRailway.Controller.Analysers.NCEPacketAnalyser.Tasks;

[Task("PacketProcessor", "Process NCE DCC Packets", "1.0")]
public class PacketProcessor(ILogger logger, ICommandStation cmdStation) : ControllerTask(logger, cmdStation), IParameterMappable {
    private readonly Queue<string>  _messageQueue    = new();
    private          string         _bufferRemainder = string.Empty;
    private          string         _lastMessage     = string.Empty;
    private          PacketAnalyser _packetAnalyser;

    protected override void Setup() {
        _packetAnalyser = new PacketAnalyser();
        SendPacketAnalyzerCommand("H2"); // We need Verbose Mode or Hex Mode? 
        SendPacketAnalyzerCommand("A+"); // We want Accessory commands
        SendPacketAnalyzerCommand("I-"); // We do not want IDLE commands
        SendPacketAnalyzerCommand("L+"); // We want Loco commands
        SendPacketAnalyzerCommand("R-"); // We do not need RESET commands
        SendPacketAnalyzerCommand("S+"); // We want Signal commands
    }

    protected override void CleanUp() { }

    protected override void DoWork() {
        var data = CommandStation?.Adapter?.RecvData() ?? null;
        if (data != null) {
            AddToQueue(data.FromByteArray());
        }

        ProcessQueue();
    }

    private void ProcessQueue() {
        foreach (var message in GetQueuedMessages()) {
            if (_lastMessage != message) {
                var             decodedPacket = _packetAnalyser.Decode(message);
                PacketTaskEvent taskEvent     = new(this, decodedPacket);
                OnTaskEvent(this, taskEvent);
            }

            _lastMessage = message;
        }
    }

    private IEnumerable<string> GetQueuedMessages() {
        while (_messageQueue.Any()) {
            yield return _messageQueue.Dequeue();
        }
    }

    private void AddToQueue(string? buffer) {
        var delimiters = new[] { "\r", "\n", "\r\n", "\n\r" };
        buffer = _bufferRemainder + buffer;
        var parts = buffer.Split(delimiters, StringSplitOptions.None);

        // If the last part does not end with a newline, store it for the next call
        if (!buffer.EndsWith("\r") && !buffer.EndsWith("\n")) {
            _bufferRemainder = parts[^1];
            parts            = parts.Take(parts.Length - 1).ToArray();
        } else {
            _bufferRemainder = string.Empty;
        }

        foreach (var part in parts) {
            if (!string.IsNullOrEmpty(part)) {
                _messageQueue.Enqueue(part);
            }
        }
    }

    private void SendPacketAnalyzerCommand(string command) {
        //A[+/-] Accessory packets on/off
        //H[0-7] Hex        mode 0-7 0=ICC mode 
        //I[+/-] idle       packets on/off     
        //L[+/-] Locomotive pkts on/off  
        //R[+/-] Resets  
        //S[+/-] Signal packets on/off   
        //V             Verbose mode 

        // We need to send multiple time, like 10 or 20, to get it to take the command
        // so loop and send. Testing shows 50ms between sends and about 50 sends. 
        for (var i = 0; i < 20; i++) {
            CommandStation?.Adapter?.SendData(command, null);
            Thread.Sleep(50); // Wait 100ms and send again 
        }
    }
}