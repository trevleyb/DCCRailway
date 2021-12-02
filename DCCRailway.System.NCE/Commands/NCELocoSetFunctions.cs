﻿using System.Text;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCELocoSetFunctions : NCECommandBase, ICmdLocoSetFunctions, ICommand {
		private readonly byte[] _opCodes = { 0x07, 0x08, 0x09, 0x15, 0x16 };

		public NCELocoSetFunctions() {
			Functions = new DCCFunctionBlocks();
			Previous = new DCCFunctionBlocks();
		}

		public NCELocoSetFunctions(int address) : this(new DCCAddress(address), new DCCFunctionBlocks()) { }

		public NCELocoSetFunctions(int address, DCCFunctionBlocks functions) : this(new DCCAddress(address), functions) { }

		public NCELocoSetFunctions(IDCCAddress address, DCCFunctionBlocks functions) {
			Address = address;
			Functions = functions;
		}

		public DCCFunctionBlocks Previous { get; set; }

		public static string Name {
			get { return "NCE Set Loco Functions"; }
		}

		public IDCCAddress Address { get; set; }
		public DCCFunctionBlocks Functions { get; }

		public override IResult Execute(IAdapter adapter) {
			IResult result;
			if (Previous == null) Previous = new DCCFunctionBlocks();

			// Loop through the 5 groups of functions and see if any have changed from last time
			// If any have changed, then sent those new settings to the command station for the Loco Address
			for (var block = 1; block <= 5; block++) {
				if (Functions.GetBlock(block) != Previous.GetBlock(block)) {
					var command = new byte[] {
						0xA2, ((DCCAddress)Address).HighAddress, ((DCCAddress)Address).LowAddress, _opCodes[block - 1],
						Functions.GetBlock(block)
					};
					result = SendAndReceieve(adapter, new NCEStandardValidation(), command);
					if (!result.OK) return result;
				}
			}

			Previous = new DCCFunctionBlocks(Functions); // save the last time we sent this 
			return new ResultOK();
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
}