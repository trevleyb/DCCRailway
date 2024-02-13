using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCommand {
    public DCCAccessoryState State   { get; set; }
}