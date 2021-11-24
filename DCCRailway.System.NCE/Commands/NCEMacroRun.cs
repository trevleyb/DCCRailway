using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEMacroRun : NCECommandBase, ICmdMacroRun {
		public string Name {
			get { return "NCE Execute Macro"; }
		}

		public byte Macro { get; set; }

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0xAC, Macro });
		}

		public override string ToString() {
			return $"RUN MACRO ({Macro})";
		}
	}
}