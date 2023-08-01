using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Utilities;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCECVRead : NCECommand, ICmdCVRead, ICommand {
    public NCECVRead(int cv = 0) {
        CV = cv;
    }

    public static string Name => "NCE ReadCV Command";

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int CV { get; set; }

    public override IResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct => 0xA9,
            DCCProgrammingMode.Paged => 0xA1,
            DCCProgrammingMode.Register => 0xA7,
            _ => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceieve(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command));
    }

    public override string ToString() {
        return $"READ CV ({CV}/{ProgrammingMode})";
    }
}