using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEDummyCmd : NCECommandBase, IDummyCmd {
		public string Name {
			get { return "NCE Dummy Command"; }
		}

		protected byte[] CommandData {
			get { return new byte[] { 0x80 }; }
		}

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), CommandData);
		}

		public override string ToString() {
			return "DUMMY CMD";
		}
	}
}