using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State   { get; set; }
}