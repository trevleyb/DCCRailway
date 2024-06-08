using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Tasks;
using Serilog;

namespace DCCRailway.Controller.Analysers.NCEPacketAnalyser.Tasks;

[Task("ProcessPackets", "Process Packets", "1.0")]
public class PacketProcessor(ILogger logger) : ControllerTask(logger), IParameterMappable {
    private readonly ILogger _logger = logger;
    private          long    count;

    protected override void DoWork() {
        // Do nothing for now
        count++;
    }
}