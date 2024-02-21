using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types; 

public interface ICmdWithAddress {
    public IDCCAddress Address { get; set; }
}