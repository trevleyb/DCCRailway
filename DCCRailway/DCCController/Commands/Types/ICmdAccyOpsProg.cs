using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCmd  {
    public byte        Value       { get; set; }
}