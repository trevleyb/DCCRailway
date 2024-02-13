using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdConsistKill : ICommand {
    public IDCCAddress Address { get; set; }
}