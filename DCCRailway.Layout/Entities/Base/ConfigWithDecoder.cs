using DCCRailway.Common.Types;

namespace DCCRailway.Layout.Entities.Base;

public class ConfigWithDecoder(DCCAddressType addressType) : ConfigBase {
    public Decoder     Decoder { get; set; } = new() { AddressType = addressType };
    public IDCCAddress Address { get; set; }
}