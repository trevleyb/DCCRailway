using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdAccySetState : ICommand,IAccyCommand {
    public IDCCAddress       Address { get; set; }
    public DCCAccessoryState State   { get; set; }
}