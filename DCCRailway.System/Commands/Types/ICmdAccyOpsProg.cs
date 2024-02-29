using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd {
    public byte Value { get; set; }
}