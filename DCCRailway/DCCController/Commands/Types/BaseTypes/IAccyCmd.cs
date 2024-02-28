using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types.BaseTypes;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}