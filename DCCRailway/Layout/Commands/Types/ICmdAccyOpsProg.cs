using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}