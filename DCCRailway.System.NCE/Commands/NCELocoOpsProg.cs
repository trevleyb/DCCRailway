using DCCRailway.Core.Common;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCELocoOpsProg : NCECommandBase, ICmdLocoOpsProg, ICommand {
		public NCELocoOpsProg() { }

		public NCELocoOpsProg(int locoAddress, DCCAddressType type, int cvAddress, byte value) {
			LocoAddress = new DCCAddress(locoAddress, type);
			CVAddress = new DCCAddress(cvAddress, DCCAddressType.CV);
			Value = value;
		}

		public NCELocoOpsProg(IDCCAddress locoAddress, IDCCAddress cvAddress, byte value) {
			LocoAddress = locoAddress;
			CVAddress = cvAddress;
			Value = value;
		}

		public static string Name {
			get { return "NCE Loco Programming on Main (POM)"; }
		}

		public IDCCAddress LocoAddress { get; set; }
		public IDCCAddress CVAddress { get; set; }
		public byte Value { get; set; }

		public override IResult Execute(IAdapter adapter) {
			var cmd = new byte[] { 0xAE };
			cmd = cmd.AddToArray(LocoAddress.AddressBytes);
			cmd = cmd.AddToArray(CVAddress.AddressBytes);
			cmd = cmd.AddToArray(Value);
			return SendAndReceieve(adapter, new NCEStandardValidation(), cmd);
		}

		public override string ToString() {
			return $"LOCO OPS PROGRAMMING ({LocoAddress}:{CVAddress}={Value})";
		}
	}
}