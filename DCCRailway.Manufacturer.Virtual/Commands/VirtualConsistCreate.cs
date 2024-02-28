﻿using System.Collections.Generic;
using System.Text;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

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