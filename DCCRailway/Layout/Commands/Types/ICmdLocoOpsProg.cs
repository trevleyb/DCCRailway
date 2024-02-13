using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCommand {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}