using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.Base;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}