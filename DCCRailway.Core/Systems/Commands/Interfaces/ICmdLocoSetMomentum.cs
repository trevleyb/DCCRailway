using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdLocoSetMomentum : ICommand {
    public IDCCAddress Address { get; set; }
    public byte Momentum { get; set; }
}