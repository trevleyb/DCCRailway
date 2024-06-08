using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.Analysers.NCEPacketAnalyser;

[Controller("NCE Packet Analyser", "NCE", "Packet Analyser", "1.0")]
public class NCEPacketAnalyser(ILogger logger) : CommandStation(logger), ICommandStation {
    public override DCCAddress CreateAddress() {
        return new DCCAddress();
    }

    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
        return new DCCAddress(address, type);
    }

    public override void Start() {
        base.Start();
    }

    public override void Stop() {
        base.Stop();
    }
}