using System;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("SetSignalAspect", "Set the Aspect of a defined Signal")]
public class NCESignalSetAspect : NCECommand, ICmdSignalSetAspect, ICommand {
    private byte _aspect;

    public NCESignalSetAspect() { }

    public NCESignalSetAspect(byte aspect = 0) {
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

    protected override ICmdResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAD };              // Command is 0xAD
        cmd = cmd.AddToArray(Address.AddressBytes); // Add the high and low bytes of the Address
        cmd = cmd.AddToArray(0x05);                 // Signals command is 0x05
        cmd = cmd.AddToArray(Aspect);               // Aspect must be 00 to 0x0f

        return SendAndReceive(adapter, new NCEStandardValidation(), cmd);
    }

    public override string ToString() {
        return $"SIGNAL ASPECT ({Address}@{Aspect})";
    }
}