using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCELocoSetSpeedSteps : NCECommandBase, ICmdLocoSetSpeedSteps, ICommand {
		public NCELocoSetSpeedSteps() { }

		public NCELocoSetSpeedSteps(int address, DCCProtocol speedSteps = DCCProtocol.DCC128) : this(new DCCAddress(address), speedSteps) { }

		public NCELocoSetSpeedSteps(IDCCAddress address, DCCProtocol speedSteps = DCCProtocol.DCC128) {
			Address = address;
			SpeedSteps = speedSteps;
		}

		public static string Name {
			get { return "NCE Set Loco Speed Steps"; }
		}

		public IDCCAddress Address { get; set; }
		public DCCProtocol SpeedSteps { get; set; }

		public override IResult Execute(IAdapter adapter) {
			byte[] command = { 0x8D };
			command = command.AddToArray(((DCCAddress)Address).AddressBytes);
			command = command.AddToArray((byte)SpeedSteps);
			return SendAndReceieve(adapter, new NCEStandardValidation(), command);
		}

		public override string ToString() {
			return $"LOCO SPEED STEPS ({Address}={SpeedSteps}";
		}
	}
}