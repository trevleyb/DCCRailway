using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.BaseTypes;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}