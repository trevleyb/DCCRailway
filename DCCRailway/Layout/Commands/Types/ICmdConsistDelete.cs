using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdConsistDelete : ICommand {
    public IDCCAddress Address { get; set; }
}