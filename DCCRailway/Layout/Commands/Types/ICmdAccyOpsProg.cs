using DCCRailway.Layout.Commands.Types.BaseTypes;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd  {
    public byte        Value       { get; set; }
}