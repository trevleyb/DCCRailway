using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdAccySetState : ICommand {
    public IDCCAddress Address { get; set; }
    public DCCAccessoryState State { get; set; }
}