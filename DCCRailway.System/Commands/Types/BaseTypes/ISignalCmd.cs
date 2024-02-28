using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.BaseTypes;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}