using System;
using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SetSignalAspect", "Set the Aspect of a defined Signal")]
public class VirtualSignalSetAspect : VirtualCommand, ICmdSignalSetAspect, ICommand {
    private byte _aspect;

    public VirtualSignalSetAspect() { }

    public VirtualSignalSetAspect(byte aspect = 0) => Aspect = aspect;

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
            } else {
                _aspect = 15;
            }
        }
    }

    public override ICommandResult Execute(IAdapter adapter) {
        var cmd = new byte[] { 0xAD };                            // Command is 0xAD
        cmd = cmd.AddToArray(((DCCAddress)Address).AddressBytes); // Add the high and low bytes of the Address
        cmd = cmd.AddToArray(0x05);                               // Signals command is 0x05
        cmd = cmd.AddToArray(Aspect);                             // Aspect must be 00 to 0x0f

        return SendAndReceive(adapter, new VirtualStandardValidation(), cmd);
    }

    public override string ToString() => $"SIGNAL ASPECT ({Address}@{Aspect})";
}