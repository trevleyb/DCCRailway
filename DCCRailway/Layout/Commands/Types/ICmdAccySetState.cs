using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCommand {
    public DCCAccessoryState State   { get; set; }
}