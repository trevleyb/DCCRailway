using DCCRailway.Common.Utilities;

namespace DCCRailway.Common.Types;

public enum DCCAddressType {
    Short,
    Long,
    Accessory,
    Signal,
    Sensor,
    Turnout,
    CV,
    Consist
}

/// <summary>
///     Represents the storage of an ADDRESS for a DCC Loco or Accessory
/// </summary>
public class DCCAddress : PropertyChangedBase, IDCCAddress {
    private const int MAX_ADDRESS = 10000;

    private int            _address;
    private DCCAddressType _addressType;
    private byte           _highAddress;
    private byte           _lowAddress;

    public DCCAddress() : this(3, DCCAddressType.Short) { }

    public DCCAddress(int address, DCCAddressType addressType = DCCAddressType.Long) {
        Address     = address;
        AddressType = addressType;
    }

    /// <summary>
    ///     For NCE, split he address into 2 parts. Return the LOW ORDER part of the address
    ///     but calculate the value first from the provided address.
    /// </summary>
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
    public byte HighAddress {
        get {
            CalculateHighLowAddress();

            return _highAddress;
        }
    }

    public byte[] AddressBytes => new[] { HighAddress, LowAddress };

    /// <summary>
    ///     Set the address but if it is > 127 then it MUST BE A LONG ADDRESS
    ///     A value of 1..127 can be considered LONG or SHORT but > 127 must be LONG
    /// </summary>
    public int Address {
        get => _address;
        set {
            if (value <= 0 || value >= MAX_ADDRESS) throw new ArgumentOutOfRangeException($"Address must be in the range of 1..{MAX_ADDRESS}");
            SetPropertyField(ref _address, value);
            if (value >= 128) AddressType = DCCAddressType.Long;
        }
    }

    public DCCAddressType AddressType {
        get => _addressType;
        set => SetPropertyField(ref _addressType, value);
    }

    public bool IsLong => AddressType == DCCAddressType.Long;

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
                _                        => "S"
            };

            return $"{Address:D4}({shortOrLong})";
        }
    }

    /// <summary>
    ///     Calculates the high & low bytes of the address and ensures, if this is a SHORT
    ///     adress that the high order byte is always 0, but if a LONG adress then both
    ///     bytes are used with the HIGH address having 11000000 added to it (highest 2 bits on)
    /// </summary>
    private void CalculateHighLowAddress() {
        if (AddressType == DCCAddressType.Short) {
            _lowAddress  = (byte)Address; // Take the low order bits
            _highAddress = 0;             // Short address is ALWAYS 0
        }
        else if (AddressType == DCCAddressType.Long) {
            _lowAddress  = (byte)Address;               // Take the low order bits
            _highAddress = (byte)(Address >> 8);        // Take the 2nd order bits
            _highAddress = (byte)(_highAddress | 0xC0); // Turn on 2 bits to indicate LONG address
        }
        else {
            _lowAddress  = (byte)Address;        // Take the low order bits
            _highAddress = (byte)(Address >> 8); // Take the 2nd order bits
        }
    }

    public override string ToString() => $"ADDRESS:{_address:D4} ({(_addressType == DCCAddressType.Short ? "Short" : "Long")}) ";
}