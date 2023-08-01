using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Utilities;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCECVWrite : NCECommand, ICmdCVWrite, ICommand {
    public NCECVWrite(int cv = 0, byte value = 0) {
        CV = cv;
        Value = value;
    }

    public static string Name => "NCE WriteCV Command";

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int CV { get; set; }
    public byte Value { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct => 0xA8,
            DCCProgrammingMode.Paged => 0xA0,
            DCCProgrammingMode.Register => 0xA6,
            _ => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceieve(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command).AddToArray(Value));
    }

    public override string ToString() {
        return $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
    }
}