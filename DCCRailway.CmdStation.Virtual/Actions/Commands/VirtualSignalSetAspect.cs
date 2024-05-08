﻿using System;
using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("SetSignalAspect", "Set the Aspect of a defined Signal")]
public class VirtualSignalSetAspect : VirtualCommand, ICmdSignalSetAspect, ICommand {
    private byte _aspect;

    public VirtualSignalSetAspect() { }

    public VirtualSignalSetAspect(byte aspect = 0) => Aspect = aspect;

    public DCCAddress Address { get; set; }

    public byte Aspect {
        get => _aspect;
        set {
            if ((value < 0 || value > 15) && value != 30 && value != 31) throw new ArgumentOutOfRangeException(nameof(value), value, "Signal Aspect must be between 0..15 or 30,31");
            _aspect = value;
        }
    }

    public bool Off {
        set {
            if (value)
                _aspect = 31;
            else
                _aspect = 15;
        }
    }

    public override string ToString() => $"SIGNAL ASPECT ({Address}@{Aspect})";
}