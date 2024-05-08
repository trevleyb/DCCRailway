using System.Collections.Generic;
using System.Text;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("ConsistCreate", "Create a Consist")]
public class NCEConsistCreate : NCECommand, ICmdConsistCreate, ICommand {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCDirection     LeadDirection  { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public DCCDirection     RearDirection  { get; set; }
    public List<DCCAddress> AddLoco        { get; } = new();

    public override ICmdResult Execute(IAdapter adapter) {
        ICmdResult result;

        // Start by deleting the current Consist by killing the consist by the lead loco
        // -----------------------------------------------------------------------------
        var killCmd = new NCEConsistKill(LeadLoco);
        result = killCmd.Execute(adapter);
        if (!result.Success) return result;

        // Add each loco to the consist
        // -----------------------------------------------------------------------------
        result = AddLocoToConsist(adapter, ConsistAddress, LeadLoco, LeadDirection, DCCConsistPosition.Front);
        if (!result.Success) return result;

        result = AddLocoToConsist(adapter, ConsistAddress, RearLoco, RearDirection, DCCConsistPosition.Rear);
        if (!result.Success) return result;

        foreach (var extraLoco in AddLoco) {
            result = AddLocoToConsist(adapter, ConsistAddress, extraLoco, DCCDirection.Forward, DCCConsistPosition.Middle);
            if (!result.Success) return result;
        }
        return result;
    }

    private static ICmdResult AddLocoToConsist(IAdapter adapter, byte consistAddress, DCCAddress address, DCCDirection direction, DCCConsistPosition position) {
        // First Delete the loco from any existing Consist
        // -----------------------------------------------
        var delCmd = new NCEConsistDelete(address);
        var delRes = delCmd.Execute(adapter);
        if (!delRes.Success) return delRes;

        var addCmd = new NCEConsistAdd(consistAddress, address, direction, position);
        var addRes = addCmd.Execute(adapter);
        if (!addRes.Success) return addRes;

        return delRes;
    }

    public override string ToString() {
        var sb = new StringBuilder();

        foreach (var loco in AddLoco) {
            sb.Append(loco + ",");
        }

        return $"CREATE CONSIST ({ConsistAddress}={LeadLoco},{sb}{RearLoco}";
    }
}