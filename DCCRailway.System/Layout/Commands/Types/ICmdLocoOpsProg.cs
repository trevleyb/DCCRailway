using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCommand {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}