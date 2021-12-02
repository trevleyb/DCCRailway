using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEConsistAdd : NCECommandBase, ICmdConsistAdd, ICommand {
		public NCEConsistAdd() { }

		public NCEConsistAdd(byte consistAddress, IDCCLoco loco, DCCConsistPosition position = DCCConsistPosition.Front) {
			Loco = loco;
			Position = position;
			ConsistAddress = consistAddress;
		}

		public NCEConsistAdd(byte consistAddress, IDCCAddress address, DCCDirection direction = DCCDirection.Forward, DCCConsistPosition position = DCCConsistPosition.Front) : this(consistAddress, new DCCLoco(address, direction), position) { }

		public static string Name {
			get { return "NCE Consist Add"; }
		}

		public byte ConsistAddress { get; set; }
		public IDCCLoco Loco { get; set; }
		public DCCConsistPosition Position { get; set; }

		public override IResult Execute(IAdapter adapter) {
			byte[] command = { 0xA2 };
			command = command.AddToArray(Loco.Address.AddressBytes);
			command = Position switch {
				DCCConsistPosition.Front => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0b : 0x0a)),
				DCCConsistPosition.Rear => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0d : 0x0c)),
				_ => command.AddToArray((byte)(Loco.Direction == DCCDirection.Forward ? 0x0f : 0x0e))
			};
			command = command.AddToArray(ConsistAddress);
			return SendAndReceieve(adapter, new NCEStandardValidation(), command);
		}

		public override string ToString() {
			return $"CONSIST ADD TO {ConsistAddress:D3} @ {Position} ({Loco.Address}={Loco.Direction})";
		}
	}
}