using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types.BaseTypes;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}