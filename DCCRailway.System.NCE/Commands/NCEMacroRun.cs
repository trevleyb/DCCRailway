using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("MacroRun", "Execute a Macro")]
public class NCEMacroRun : NCECommand, ICmdMacroRun {
    public byte Macro { get; set; }

    public override IResult Execute(IAdapter adapter) => SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });

    public override string ToString() => $"RUN MACRO ({Macro})";
}