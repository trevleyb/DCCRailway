using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdConsistDelete : ICommand {
    public IDCCAddress Address { get; set; }
}