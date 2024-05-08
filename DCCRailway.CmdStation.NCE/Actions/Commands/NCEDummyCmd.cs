using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("DummyCmd", "A Dummy Command that does not do anything")]
public class NCEDummyCmd : NCECommand, IDummyCmd {
    protected byte[] CommandData => new byte[] { 0x80 };

    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), CommandData);

    public override string ToString() => "DUMMY CMD";
}