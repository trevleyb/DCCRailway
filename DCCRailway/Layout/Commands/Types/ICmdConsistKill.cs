using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdConsistKill : ICommand {
    public IDCCAddress Address { get; set; }
}