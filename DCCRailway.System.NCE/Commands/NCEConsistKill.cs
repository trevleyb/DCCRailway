using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEConsistKill : NCECommandBase, ICmdConsistKill, ICommand {
		public NCEConsistKill() { }

		public NCEConsistKill(IDCCLoco loco) : this(loco.Address) { }

		public NCEConsistKill(IDCCAddress address) {
			Address = address;
		}

		public IDCCAddress Address { get; set; }

		public static string Name {
			get { return "NCE Consist Kill"; }
		}

		public override IResult Execute(IAdapter adapter) {
			byte[] command = { 0xA2 };
			command = command.AddToArray(Address.AddressBytes);
			command = command.AddToArray(0x11);
			command = command.AddToArray(0);
			return SendAndReceieve(adapter, new NCEStandardValidation(), command);
		}

		public override string ToString() {
			return $"CONSIST KILL ({Address})";
		}
	}
}