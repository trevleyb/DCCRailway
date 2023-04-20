using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdLocoSetSpeed : ICommand {
    public IDCCAddress Address { get; set; }
    public DCCProtocol SpeedSteps { get; set; }
    public DCCDirection Direction { get; set; }
    public byte Speed { get; set; }
}