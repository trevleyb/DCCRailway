using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEAccySetState : NCECommandBase, ICmdAccySetState, ICommand {
		public NCEAccySetState() { }

		public NCEAccySetState(DCCAccessoryState state = DCCAccessoryState.Normal) {
			State = state;
		}

		public static string Name {
			get { return "NCE Set Accessory State"; }
		}

		public IDCCAddress Address { get; set; }
		public DCCAccessoryState State { get; set; }

		public override IResult Execute(IAdapter adapter) {
			var cmd = new byte[] { 0xAD }; // Command is 0xAD
			cmd = cmd.AddToArray(((DCCAddress)Address).AddressBytes); // Add the high and low bytes of the Address
			cmd = cmd.AddToArray((byte)(State == DCCAccessoryState.On ? 0x03 : 0x04)); // Normal=0x03, Thrown=0x04
			cmd = cmd.AddToArray(0); // Accessory always has a data of 0x00
			return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
		}

		public override string ToString() {
			return $"ACCY STATE ({Address} = {State})";
		}
	}
}