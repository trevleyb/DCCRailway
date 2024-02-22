using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;
using DCCRailway.Utilities.Exceptions;

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("CVWrite", "Write a value to a CV on a Loco")]
public class VirtualCVWrite : VirtualCommand, ICmdCVWrite {
    public VirtualCVWrite(int cv = 0, byte value = 0) {
        CV    = cv;
        Value = value;
    }

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }
    public byte               Value           { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct   => 0xA8,
            DCCProgrammingMode.Paged    => 0xA0,
            DCCProgrammingMode.Register => 0xA6,
            _                           => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceive(adapter, new VirtualDataReadValidation(), CV.ToByteArray().AddToArray(command).AddToArray(Value));
    }

    public override string ToString() => $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
    public          IDCCAddress? Address    { get; set; }
}