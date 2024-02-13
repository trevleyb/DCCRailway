using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCommand {
    public DCCAccessoryState State   { get; set; }
}