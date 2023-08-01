﻿using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Utilities;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

public class NCESignalSetAspect : NCECommand, ICmdSignalSetAspect, ICommand {
    private byte _aspect;

    public NCESignalSetAspect() { }

    public NCESignalSetAspect(byte aspect = 0) {
        Aspect = aspect;
    }

    public static string Name => "NCE Set Signal Aspect";

    public IDCCAddress Address { get; set; }

    public byte Aspect {
        get => _aspect;
        set {
            if ((value < 0 || value > 15) && value != 30 && value != 31) throw new ArgumentOutOfRangeException(nameof(value), value, "Signal Aspect must be between 0..15 or 30,31");
            _aspect = value;
        }
    }

    public bool Off {
        set {
            if (value) {
                _aspect = 31;
            }
            else {
                _aspect = 15;
            }
        }
    }

    public override IResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAD }; // Command is 0xAD
        cmd = cmd.AddToArray(((DCCAddress)Address).AddressBytes); // Add the high and low bytes of the Address
        cmd = cmd.AddToArray(0x05); // Signals command is 0x05
        cmd = cmd.AddToArray(Aspect); // Aspect must be 00 to 0x0f

        return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() {
        return $"SIGNAL ASPECT ({Address}@{Aspect}";
    }
}