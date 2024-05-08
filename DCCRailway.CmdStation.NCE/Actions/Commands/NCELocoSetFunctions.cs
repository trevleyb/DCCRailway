﻿using System.Text;
using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.NCE.Actions.Validators;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("LocoSetFunctions", "Set the Loco Functions")]
public class NCELocoSetFunctions : NCECommand, ICmdLocoSetFunctions, ICommand {
    private readonly byte[] _opCodes = { 0x07, 0x08, 0x09, 0x15, 0x16 };

    public NCELocoSetFunctions() {
        Functions = new DCCFunctionBlocks();
        Previous  = new DCCFunctionBlocks();
    }

    public NCELocoSetFunctions(int address) : this(new DCCAddress(address), new DCCFunctionBlocks()) { }

    public NCELocoSetFunctions(int address, DCCFunctionBlocks functions) : this(new DCCAddress(address), functions) { }

    public NCELocoSetFunctions(DCCAddress address, DCCFunctionBlocks functions) {
        Address   = address;
        Functions = functions;
    }

    public DCCFunctionBlocks? Previous  { get; set; }
    public DCCAddress        Address   { get; set; }
    public DCCFunctionBlocks  Functions { get; }

    public override ICmdResult Execute(IAdapter adapter) {
        Previous ??= new DCCFunctionBlocks();

        // Loop through the 5 groups of functions and see if any have changed from last time
        // If any have changed, then sent those new settings to the command station for the Loco Address
        for (var block = 1; block <= 5; block++) {
            if (Functions.GetBlock(block) != Previous.GetBlock(block)) {
                var command = new byte[] { 0xA2, ((DCCAddress)Address).HighAddress, ((DCCAddress)Address).LowAddress, _opCodes[block - 1], Functions.GetBlock(block) };
                var result  = SendAndReceive(adapter, new NCEStandardValidation(), command);

                if (!result.Success) return result;
            }
        }

        Previous = new DCCFunctionBlocks(Functions); // save the last time we sent this 

        return CmdResult.Ok();
    }

    public override string ToString() {
        StringBuilder sb = new();

        for (var i = 0; i < 28; i++) {
            sb.Append($"F{i:D2}={(Functions[i] ? "1" : "0")},");
        }

        sb.Append($"F28={(Functions[28] ? "1" : "0")}");

        return $"LOCO FUNCTIONS ({Address} / {sb}";
    }
}