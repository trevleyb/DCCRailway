using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEMacroRun : NCECommand, ICmdMacroRun {
    public string Name => "NCE Execute Macro";

    public byte Macro { get; set; }

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });
    }

    public override string ToString() {
        return $"RUN MACRO ({Macro})";
    }
}