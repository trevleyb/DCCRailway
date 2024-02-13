using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Commands;
using DCCRailway.System.Layout.Commands.Results;
using DCCRailway.System.Layout.Commands.Types;
using DCCRailway.System.Layout.Types;
using DCCRailway.System.NCE.Commands.Validators;
using DCCRailway.System.Utilities;
using DCCRailway.System.Utilities.Exceptions;

namespace DCCRailway.System.NCE.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class NCECVRead : NCECommand, ICmdCVRead, ICommand {
    public NCECVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override ICommandResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct   => 0xA9,
            DCCProgrammingMode.Paged    => 0xA1,
            DCCProgrammingMode.Register => 0xA7,
            _                           => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceive(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command));
    }

    public override string ToString() => $"READ CV ({CV}/{ProgrammingMode})";
}