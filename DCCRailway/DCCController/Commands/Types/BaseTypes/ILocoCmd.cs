using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types.BaseTypes;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}