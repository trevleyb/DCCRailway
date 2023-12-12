using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoOpsProg : ICommand, ILocoCommand {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}