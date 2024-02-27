using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types.BaseTypes;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}