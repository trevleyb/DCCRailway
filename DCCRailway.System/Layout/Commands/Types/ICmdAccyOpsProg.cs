using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}