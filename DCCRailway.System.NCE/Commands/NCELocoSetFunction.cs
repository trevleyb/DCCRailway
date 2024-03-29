using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

[Command("LocoSetFunction", "Set a specific function on a Loco")]
public class NCELocoSetFunction : NCECommand, ICmdLocoSetFunction, ICommand {
    private readonly byte[] _opCodes = { 0x07, 0x08, 0x09, 0x15, 0x16 };

    public byte Function { get; set; }
    public bool State    { get; set; }
    
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
    
    public NCELocoSetFunction(IDCCAddress address, DCCFunctionBlocks functions) {
        Address   = address;
        Functions = functions;
    }

    public DCCFunctionBlocks? Previous  { get; set; }
    public IDCCAddress        Address   { get; set; }
    public DCCFunctionBlocks  Functions { get; }

    public override ICommandResult Execute(IAdapter adapter) {
        Previous ??= new DCCFunctionBlocks();

        Functions[Function] = State;
        
        // Loop through the 5 groups of functions and see if any have changed from last time
        // If any have changed, then sent those new settings to the command station for the Loco Address
        for (var block = 1; block <= 5; block++) {
            if (Functions.GetBlock(block) != Previous.GetBlock(block)) {
                var command = new byte[] { 0xA2, ((DCCAddress)Address).HighAddress, ((DCCAddress)Address).LowAddress, _opCodes[block - 1], Functions.GetBlock(block) };
                var result  = SendAndReceive(adapter, new NCEStandardValidation(), command);
                if (!result.IsOK) return result;
            }
        }

        Previous = new DCCFunctionBlocks(Functions); // save the last time we sent this 
        return CommandResult.Success();
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