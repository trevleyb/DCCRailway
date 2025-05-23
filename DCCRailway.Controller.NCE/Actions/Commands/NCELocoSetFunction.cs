using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("LocoSetFunction", "Set a specific function on a Loco")]
public class NCELocoSetFunction : NCECommand, ICmdLocoSetFunction, ICommand {
    private readonly byte[] _opCodes = { 0x07, 0x08, 0x09, 0x15, 0x16 };

    public NCELocoSetFunction() {
        Functions = new DCCFunctionBlocks();
        Previous  = new DCCFunctionBlocks();
    }

    public NCELocoSetFunction(int address) : this(new DCCAddress(address), new DCCFunctionBlocks()) { }

    public NCELocoSetFunction(int address, DCCFunctionBlocks functions) : this(new DCCAddress(address), functions) { }

    public NCELocoSetFunction(int address, byte function, bool state) : this(address) {
        Function = function;
        State    = state;
    }

    public NCELocoSetFunction(DCCAddress address, DCCFunctionBlocks functions) {
        Address   = address;
        Functions = functions;
    }

    public DCCFunctionBlocks? Previous  { get; set; }
    public DCCFunctionBlocks  Functions { get; set; }

    public byte       Function { get; set; }
    public bool       State    { get; set; }
    public DCCAddress Address  { get; set; }

    protected override ICmdResult Execute(IAdapter adapter) {
        // We only want to send the function settings that have changed since the last time
        // But we can't easily tell what has changed, so we need to get the last settings
        // The only place to store this is in the Adapter, so we need to check if the adapter supports this
        // -------------------------------------------------------------------------------------------
        Functions = CommandStation.FunctionBlocks(Address);
        Previous  = CommandStation.FunctionBlocks(Address).Clone();
        Functions.SetFunction(Function, State);

        // Loop through the 5 groups of functions and see if any have changed from last time
        // If any have changed, then send those new settings to the command station for the Loco Address
        for (var block = 1; block <= 5; block++) {
            if (Functions.GetBlock(block) != Previous.GetBlock(block)) {
                var command = new byte[] {
                    0xA2, Address.HighAddress, ((DCCAddress)Address).LowAddress, _opCodes[block - 1],
                    Functions.GetBlock(block)
                };

                var result = SendAndReceive(adapter, new NCEStandardValidation(), command);
                if (!result.Success) return result;
            }
        }

        return CmdResult.Ok();
    }

    public override string ToString() {
        StringBuilder sb = new();

        for (var i = 0; i < 28; i++) sb.Append($"F{i:D2}={(Functions[i] ? "1" : "0")},");

        sb.Append($"F28={(Functions[28] ? "1" : "0")}");

        return $"LOCO FUNCTIONS ({Address} / {sb}";
    }
}