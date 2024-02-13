using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}