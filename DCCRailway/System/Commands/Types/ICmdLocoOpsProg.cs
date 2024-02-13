using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCommand {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}