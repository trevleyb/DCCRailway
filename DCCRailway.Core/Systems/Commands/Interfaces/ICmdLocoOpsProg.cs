using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdLocoOpsProg : ICommand {
    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress { get; set; }
    public byte Value { get; set; }
}