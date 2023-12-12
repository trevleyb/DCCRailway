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

    public override IResultOld Execute(IAdapter adapter) {
        IResultOld resultOld;

        // Start by deleting the current Consist by killing the consist by the lead loco
        // -----------------------------------------------------------------------------
        var killCmd = new NCEConsistKill(LeadLoco);
        resultOld = killCmd.Execute(adapter);

        if (!resultOld.OK) return resultOld;

        // Add each loco to the consist
        // -----------------------------------------------------------------------------
        resultOld = AddLocoToConsist(adapter, ConsistAddress, LeadLoco, DCCConsistPosition.Front);

        if (!resultOld.OK) return resultOld;

        resultOld = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Rear);

        if (!resultOld.OK) return resultOld;

        foreach (var extraLoco in AddLoco) {
            resultOld = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Middle);

            if (!resultOld.OK) return resultOld;
        }

        return new ResultOldOk();
    }

    private static IResultOld AddLocoToConsist(IAdapter adapter, byte consistAddress, IDCCLoco loco, DCCConsistPosition position) {
        // First Delete the loco from any existing Consist
        // -----------------------------------------------
        var delCmd = new NCEConsistDelete(loco);
        var delRes = delCmd.Execute(adapter);

        if (!delRes.OK) return delRes;

        var addCmd = new NCEConsistAdd(consistAddress, loco, position);
        var addRes = addCmd.Execute(adapter);

        if (!addRes.OK) return addRes;

        return new ResultOldOk();
    }

    public override string ToString() {
        var sb = new StringBuilder();

        foreach (var loco in AddLoco) {
            sb.Append(loco + ",");
        }

        return $"CREATE CONSIST ({ConsistAddress}={LeadLoco},{sb}{RearLoco}";
    }
}