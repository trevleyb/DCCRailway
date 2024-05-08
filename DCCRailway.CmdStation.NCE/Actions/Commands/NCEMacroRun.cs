using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("MacroRun", "Execute a Macro")]
public class NCEMacroRun : NCECommand, ICmdMacroRun {
    public byte Macro { get; set; }

    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });

    public override string ToString() => $"RUN MACRO ({Macro})";
}