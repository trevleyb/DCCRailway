using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public IDCCAddress LocoAddress { get; set; }
    public byte        Value       { get; set; }
}