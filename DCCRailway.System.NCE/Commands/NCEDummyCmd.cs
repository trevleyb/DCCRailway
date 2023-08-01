using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEDummyCmd : NCECommand, IDummyCmd {
    public string Name => "NCE Dummy Command";

    protected byte[] CommandData {
        get { return new byte[] { 0x80 }; }
    }

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), CommandData);
    }

    public override string ToString() {
        return "DUMMY CMD";
    }
}