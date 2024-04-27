using System;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.Station.Virtual.Simulator;

/// <summary>
///     Represents a Loco that is being simulated
/// </summary>
/// TODO: Need to make this serializable so we can save/restore Simulated Settings
[Serializable]
public class SimulatedLocoEntry {
    private const int     MAX_CV_VALUE = 2048;
    private       byte?[] _cvValues;

    public SimulatedLocoEntry(DCCAddress Address) {
        this.Address = Address;

        _cvValues = new byte?[MAX_CV_VALUE];

        for (var i = 0; i < _cvValues.Length; i++) {
            _cvValues[i] = null;
        }

        // Setup the common CV Values so we can report stuff correctly
        // -----------------------------------------------------------
        this[7] = 123; // Manufacturers Version Number
        this[8] = 123; // Manufacturers ID

        this[2] = 0; // VStart
        this[3] = 0; // Acceleration rate
        this[4] = 0; // Deceleration Rate
        this[5] = 0; // VHigh
        this[6] = 0; // VMid

        if (Address.AddressType == DCCAddressType.Long) {
            this[29] = this[29].SetBit(5, true);
            this[1]  = 3;
            this[17] = Address.HighAddress;
            this[18] = Address.LowAddress;
        }
        else {
            this[1]  = (byte)Address.Address;
            this[17] = 0;
            this[18] = 0;
            this[29] = this[29].SetBit(5, false);
        }
    }

    public byte              Speed        { get; set; }
    public int               SignalAspect { get; set; }
    public DCCAccessoryState State        { get; set; }
    public DCCFunctionBlocks Functions    { get; set; }
    public DCCAddress        Address      { get; set; }

    public DCCAddressType Type => Address.AddressType;

    public DCCDirection Direction {
        get => (this[29] & 0b00000001) == 1 ? DCCDirection.Forward : DCCDirection.Reverse;
        set => this[29] = this[29].SetBit(0, Direction == DCCDirection.Forward ? true : false);
    }

    public byte this[int cvAddress] {
        get {
            if (cvAddress >= 0 && cvAddress <= MAX_CV_VALUE) return (byte)(_cvValues[cvAddress] == null ? 123 : _cvValues[cvAddress]!);

            throw new IndexOutOfRangeException($"CV Address must be between 0...{MAX_CV_VALUE}");
        }
        set {
            if (cvAddress >= 0 && cvAddress <= MAX_CV_VALUE) _cvValues[cvAddress] = value;

            throw new IndexOutOfRangeException($"CV Address must be between 0...{MAX_CV_VALUE}");
        }
    }

    public int ConsistAddress() => this[19].SetBit(7, false);

    public bool IsInConsist(int consistAddress) => ConsistAddress() != 0;

    public void ClearConsist() => this[29] = 0;

    public void SetConsist(int consistAddress, DCCDirection direction) {
        this[19] = (byte)consistAddress;
        this[19] = this[19].SetBit(7, direction == DCCDirection.Forward ? false : true);
    }
}