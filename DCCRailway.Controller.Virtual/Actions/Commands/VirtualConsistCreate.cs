using System.Collections.Generic;
using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("ConsistCreate", "Create a Consist")]
public class VirtualConsistCreate : VirtualCommand, ICmdConsistCreate, ICommand {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCDirection     LeadDirection  { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public DCCDirection     RearDirection  { get; set; }
    public List<DCCAddress> AddLoco        { get; } = new();

    public override string ToString() {
        var sb = new StringBuilder();

        foreach (var loco in AddLoco) {
            sb.Append(loco + ",");
        }

        return $"CREATE CONSIST ({ConsistAddress}={LeadLoco},{sb}{RearLoco}";
    }
}