using DCCPacketAnalyser.Analyser.Base;
using DCCRailway.Controller.Tasks;
using DCCRailway.Controller.Tasks.Events;

namespace DCCRailway.Controller.Analysers.NCEPacketAnalyser.Tasks;

public class PacketTaskEvent(IControllerTask? task, IPacketMessage packetMessage) : TaskEvent(task), ITaskEvent {
    public IPacketMessage PacketMessage { get; private set; } = packetMessage;
}