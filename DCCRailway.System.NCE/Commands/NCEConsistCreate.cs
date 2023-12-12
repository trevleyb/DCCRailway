using System.Collections.Generic;
using System.Text;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

[Command("ConsistCreate", "Create a Consist")]
public class NCEConsistCreate : NCECommand, ICmdConsistCreate, ICommand {
    public byte           ConsistAddress { get; set; }
    public IDCCLoco       LeadLoco       { get; set; }
    public IDCCLoco       RearLoco       { get; set; }
    public List<IDCCLoco> AddLoco        { get; } = new();

    public override ICommandResult Execute(IAdapter adapter) {
        ICommandResult result;

        // Start by deleting the current Consist by killing the consist by the lead loco
        // -----------------------------------------------------------------------------
        var killCmd = new NCEConsistKill(LeadLoco);
        result = killCmd.Execute(adapter);

        if (!result.IsOK) return result;

        // Add each loco to the consist
        // -----------------------------------------------------------------------------
        result = AddLocoToConsist(adapter, ConsistAddress, LeadLoco, DCCConsistPosition.Front);
        if (!result.IsOK) return result;

        result = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Rear);
        if (!result.IsOK) return result;

        foreach (var extraLoco in AddLoco) {
            result = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Middle);
            if (!result.IsOK) return result;
        }

        return CommandResult.Success();
    }

    private static ICommandResult AddLocoToConsist(IAdapter adapter, byte consistAddress, IDCCLoco loco, DCCConsistPosition position) {
        // First Delete the loco from any existing Consist
        // -----------------------------------------------
        var delCmd = new NCEConsistDelete(loco);
        var delRes = delCmd.Execute(adapter);

        if (!delRes.IsOK) return delRes;

        var addCmd = new NCEConsistAdd(consistAddress, loco, position);
        var addRes = addCmd.Execute(adapter);

        if (!addRes.IsOK) return addRes;

        return CommandResult.Success();
    }

    public override string ToString() {
        var sb = new StringBuilder();

        foreach (var loco in AddLoco) {
            sb.Append(loco + ",");
        }

        return $"CREATE CONSIST ({ConsistAddress}={LeadLoco},{sb}{RearLoco}";
    }
}