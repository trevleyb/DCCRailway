namespace DCCRailway.System.Layout.Types;

public interface IDCCAddress {
    int            Address     { get; set; }
    DCCAddressType AddressType { get; set; }
    bool           IsLong      { get; }
    string         AddressName { get; }

    byte   LowAddress   { get; }
    byte   HighAddress  { get; }
    byte[] AddressBytes { get; }
}