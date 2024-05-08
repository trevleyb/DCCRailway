using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Exceptions;
using DCCRailway.CmdStation.NCE.Actions.Validators;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class NCECVRead : NCECommand, ICmdCVRead, ICommand {
    public NCECVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override ICmdResult Execute(IAdapter adapter) {
        byte command = ProgrammingMode switch {
            DCCProgrammingMode.Direct   => 0xA9,
            DCCProgrammingMode.Paged    => 0xA1,
            DCCProgrammingMode.Register => 0xA7,
            _                           => throw new UnsupportedCommandException("Invalid CV access type provided.")
        };

        return SendAndReceive(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command));
    }

    public override string       ToString() => $"READ CV ({CV}/{ProgrammingMode})";
    public          DCCAddress? Address    { get; set; }
}