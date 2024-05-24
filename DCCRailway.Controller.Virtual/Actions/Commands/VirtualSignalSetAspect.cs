using System;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("SetSignalAspect", "Set the Aspect of a defined Signal")]
public class VirtualSignalSetAspect : VirtualCommand, ICmdSignalSetAspect, ICommand {
    private byte _aspect;

    public VirtualSignalSetAspect() { }

    public VirtualSignalSetAspect(byte aspect = 0) {
        Aspect = aspect;
    }

    public DCCAddress Address { get; set; }

    public byte Aspect {
        get => _aspect;
        set {
            if ((value < 0 || value > 15) && value != 30 && value != 31)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Signal Aspect must be between 0..15 or 30,31");
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

    public override string ToString() {
        return $"SIGNAL ASPECT ({Address}@{Aspect})";
    }
}