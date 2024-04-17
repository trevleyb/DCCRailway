using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.Base;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}