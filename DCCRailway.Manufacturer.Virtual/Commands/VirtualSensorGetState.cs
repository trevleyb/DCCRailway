﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Results;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

[assembly: InternalsVisibleTo("DCCRailway.Delete.Test.VirtualPowerCabSensorTests")]

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState {
    private readonly SensorCache _sensorCache = new();

    public VirtualSensorGetState() { }

    public VirtualSensorGetState(byte cab, byte pin) => SetAddressByCabPin(cab, pin);

    public VirtualSensorGetState(int address) => SensorAddress = new DCCAddress(address, DCCAddressType.Accessory);

    public VirtualSensorGetState(IDCCAddress address) {
        SensorAddress             = address;
        SensorAddress.AddressType = DCCAddressType.Accessory;
    }

    public IDCCAddress Address       => SensorAddress;
    public IDCCAddress SensorAddress { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        if (!_sensorCache.IsCurrent) {
            var result = SendAndReceive(adapter, new VirtualSensorValidator(), new byte[] { 0x9B, CalculateCabPin(SensorAddress).cab });
            if (result.IsFailure) return result;
            _sensorCache.UpdateCache(CalculateCabPin(SensorAddress).cab, result!.Data?.Data);
        }
        return new VirtualCommandResultSensorState(Address, _sensorCache.GetState(SensorAddress.Address));
    }

    public void SetAddressByCabPin(byte cab, byte pin) => SensorAddress = new DCCAddress(CalculateAddress(cab, pin), DCCAddressType.Accessory);

    protected internal static (byte cab, byte pin) CalculateCabPin(IDCCAddress address) => CalculateCabPin(address.Address);

    protected internal static (byte cab, byte pin) CalculateCabPin(int address) {
        var pin = address % 16 + 1;
        var cab = (address - address % 16) / 16 + 1;

        return ((byte)cab, (byte)pin);
    }

    protected internal static int CalculateAddress(byte cab, byte pin) =>

        // Formula (copied from JMRI) is :
        (cab - 1) * 16 + (pin - 1);

    public override string ToString() => $"GET SENSOR STATE ({SensorAddress})";

    /// <summary>
    ///     A simple cache of the data so we don't need to continuously go to the command
    ///     station and request the data. This should make the process much faster.
    ///     Cache is set to a livetime of a maximum of 1 second so only good when
    ///     we need to pull all the states quickly.
    /// </summary>
    protected class SensorCache {
        private const int                     CACHE_VALIDITY = 1000;
        protected     DateTime                lastUpdated;
        protected     Dictionary<int, byte[]> sensorEntry;

        public SensorCache() {
            lastUpdated = DateTime.MinValue;
            sensorEntry = new Dictionary<int, byte[]>();
        }

        public bool IsCurrent {
            get {
                var ts        = DateTime.Now - lastUpdated;
                var isCurrent = ts.TotalMilliseconds < CACHE_VALIDITY;

                return isCurrent;
            }
        }

        public void UpdateCache(byte cab, byte[]? data) {
            sensorEntry = new Dictionary<int, byte[]> { { cab, data ?? new byte[] { 0, 0 } } };
            lastUpdated = DateTime.Now;
        }

        public bool GetState(int address) {
            var cab = CalculateCabPin(address).cab;
            var pin = CalculateCabPin(address).pin;

            return GetState(cab, pin);
        }

        public bool GetState(byte cab, byte pin) {
            byte? pinCheck;
            byte? pinValue;

            // Virtual Seems to return a 1=Not Occupied and a 0=Occupied so flip these around
            // Virtual seems to return that value if none are occupied as 00111111 11111111
            // Because there is a maximum of 14 PINs so the last 2 are ignored. 
            // so we AND 11000000 to get 11111111 11111111 and then flip it to 00000000 00000000
            // If pin 5 was set we would get 00111111 11101111 which AND/FLIP = 00000000 00010000
            // If pin 12 was set 00110111 11111111 so AND/FLIP = 00001000 00000000
            // ---------------------------------------------------------------------------
            try {
                if (sensorEntry.TryGetValue(cab, out var data)) {
                    if (pin >= 1 && pin <= 8) {
                        pinCheck = new byte().SetBit(pin - 1, true);
                        pinValue = data?[1].Invert();
                    } else if (pin >= 9 && pin <= 16) {
                        pinCheck = new byte().SetBit(pin - 9, true);
                        pinValue = (byte?)(data?[0].Invert() - 0xC0);
                    } else {
                        return false;
                    }

                    return (pinValue & pinCheck) > 0;
                }

                return false;
            } catch {
                return false;
            }
        }
    }
}