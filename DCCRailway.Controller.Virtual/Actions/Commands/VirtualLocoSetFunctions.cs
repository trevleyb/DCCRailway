using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("LocoSetFunctions", "Set the Loco Functions")]
public class VirtualLocoSetFunctions : VirtualCommand, ICmdLocoSetFunctions, ICommand {
    private readonly byte[] _opCodes = { 0x07, 0x08, 0x09, 0x15, 0x16 };

    public VirtualLocoSetFunctions() {
        Functions = new DCCFunctionBlocks();
        Previous  = new DCCFunctionBlocks();
    }

    public VirtualLocoSetFunctions(int address) : this(new DCCAddress(address), new DCCFunctionBlocks()) { }

    public VirtualLocoSetFunctions(int address, DCCFunctionBlocks functions) : this(new DCCAddress(address), functions) { }

    public VirtualLocoSetFunctions(DCCAddress address, DCCFunctionBlocks functions) {
        Address   = address;
        Functions = functions;
    }

    public DCCFunctionBlocks? Previous  { get; set; }
    public DCCAddress         Address   { get; set; }
    public DCCFunctionBlocks  Functions { get; }

    public override string ToString() {
        StringBuilder sb = new();

        for (var i = 0; i < 28; i++) sb.Append($"F{i:D2}={(Functions[i] ? "1" : "0")},");

        sb.Append($"F28={(Functions[28] ? "1" : "0")}");

        return $"LOCO FUNCTIONS ({Address} / {sb}";
    }
}