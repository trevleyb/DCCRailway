using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State   { get; set; }
}