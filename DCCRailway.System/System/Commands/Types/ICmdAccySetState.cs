using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdAccySetState : ICommand, IAccyCommand {
    public DCCAccessoryState State   { get; set; }
}