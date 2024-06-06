using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Types;

/// <summary>
///     Represents the storage of an ADDRESS for a DCC Loco or Accessory
/// </summary>
[Serializable]
public class DCCAddress : PropertyChangedBase, IEqualityComparer<DCCAddress> {
    private const int MAX_ADDRESS = 10000;

    private int            _address;
    private DCCAddressType _addressType;
    private byte           _highAddress;
    private byte           _lowAddress;
    private DCCProtocol    _protocol;

    public DCCAddress() : this(3, DCCAddressType.Short) { }

    // Helper constructor to allow a string to be passed in as a string value of the address
    // Strip away all non-numeric values as in some cases we my have a Char-prefix
    public DCCAddress(string address) {
        Address = int.TryParse(Regex.Replace(address, @"\D", ""), out var addressVal) ? addressVal : 0;
    }

    public DCCAddress(int address, DCCAddressType addressType = DCCAddressType.Long, DCCProtocol protocol = DCCProtocol.DCC28) {
        Address     = address;
        AddressType = addressType;
        Protocol    = protocol;
    }

    /// <summary>
    ///     For NCE, split he address into 2 parts. Return the LOW ORDER part of the address
    ///     but calculate the value first from the provided address.
    /// </summary>
    [JsonIgnore]
    public byte LowAddress {
        get {
            CalculateHighLowAddress();
            return _lowAddress;
        }
    }

    /// <summary>
    ///     For NCE, split he address into 2 parts. Return the HIGH ORDER part of the address
    ///     but calculate the value first from the provided address.
    /// </summary>
    [JsonIgnore]
    public byte HighAddress {
        get {
            CalculateHighLowAddress();
            return _highAddress;
        }
    }

    [JsonIgnore] public byte[] AddressBytes => new[] { HighAddress, LowAddress };

    /// <summary>
    ///     Set the address but if it is > 127 then it MUST BE A LONG ADDRESS
    ///     A value of 1..127 can be considered LONG or SHORT but > 127 must be LONG
    /// </summary>
    public int Address {
        get => _address;
        set {
            if (value < 0 || value >= MAX_ADDRESS)
                throw new ArgumentOutOfRangeException($"Address must be in the range of 1..{MAX_ADDRESS}");

            SetPropertyField(ref _address, value);
            if (value >= 128) AddressType = DCCAddressType.Long;
            if (value == 0) AddressType   = DCCAddressType.Broadcast;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DCCAddressType AddressType {
        get => _addressType;
        set => SetPropertyField(ref _addressType, value);
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DCCProtocol Protocol {
        get => _protocol;
        set => SetPropertyField(ref _protocol, value);
    }

    [JsonIgnore] public bool IsLong => AddressType == DCCAddressType.Long;

    [JsonIgnore]
    public string AddressName {
        get {
            var shortOrLong = AddressType switch {
                DCCAddressType.Short     => "S",
                DCCAddressType.Long      => "L",
                DCCAddressType.Accessory => "ACCY",
                DCCAddressType.Signal    => "SIG",
                DCCAddressType.Sensor    => "SEN",
                DCCAddressType.Turnout   => "T",
                DCCAddressType.CV        => "CV",
                DCCAddressType.Consist   => "CON",
                DCCAddressType.Broadcast => "BRC",
                _                        => "S"
            };

            return $"{Address:D4}({shortOrLong})";
        }
    }

    public bool Equals(DCCAddress? x, DCCAddress? y) {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
        return x.Address == y.Address && x.AddressType == y.AddressType && x.Protocol == y.Protocol;
    }

    public int GetHashCode(DCCAddress obj) {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = obj.Address.GetHashCode();
            hashCode = (hashCode * 397) ^ obj.AddressType.GetHashCode();
            hashCode = (hashCode * 397) ^ obj.Protocol.GetHashCode();
            return hashCode;
        }
    }

    /// <summary>
    ///     Calculates the high & low bytes of the address and ensures, if this is a SHORT
    ///     adress that the high order byte is always 0, but if a LONG adress then both
    ///     bytes are used with the HIGH address having 11000000 added to it (highest 2 bits on)
    ///     but only if the address type is a LOCO
    /// </summary>
    private void CalculateHighLowAddress() {
        if (AddressType == DCCAddressType.Short) {
            _lowAddress  = (byte)(Address & 0b11111111); // Take the low order bits
            _highAddress = 0;                            // Short address is ALWAYS 0
        } else if (AddressType == DCCAddressType.Long) {
            _lowAddress  = (byte)(Address & 0b11111111); // Take the low order bits
            _highAddress = (byte)(Address >> 8);         // Take the 2nd order bits
            _highAddress = (byte)(_highAddress | 0xC0);  // Turn on 2 bits to indicate LONG address
        } else {
            _lowAddress  = (byte)(Address & 0b11111111); // Take the low order bits
            _highAddress = (byte)(Address >> 8);         // Take the 2nd order bits
        }
    }

    public override string ToString() {
        return $"{_address:D4} ({(_addressType == DCCAddressType.Short ? "Short" : "Long")}) ";
    }
}