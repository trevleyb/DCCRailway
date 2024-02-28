using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types.BaseTypes;

public interface ICVCmd {
    public int          CV      { get; }
    public IDCCAddress? Address { get; set; }
}