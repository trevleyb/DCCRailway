using DCCRailway.System.Types;

namespace DCCRailway.Configuration.Base;

public class ConfigWithDecoder(DCCAddressType addressType) : ConfigBase {
    public Decoder Decoder { get; set; } = new() { AddressType = addressType };
}