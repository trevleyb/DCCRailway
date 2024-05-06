using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Exceptions;
using DCCRailway.CmdStation.NCE.Commands.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("CVWrite", "Write a value to a CV on a Loco")]
public class NCECVWrite : NCECommand, ICmdCVWrite, ICommand {
    public NCECVWrite(int cv = 0, byte value = 0) {
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

        return SendAndReceive(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command).AddToArray(Value));
    }

    public override string       ToString() => $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
    public          IDCCAddress? Address    { get; set; }
}