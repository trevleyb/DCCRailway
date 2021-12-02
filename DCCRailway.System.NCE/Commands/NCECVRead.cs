using DCCRailway.Core.Common;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCECVRead : NCECommandBase, ICmdCVRead, ICommand {
		public NCECVRead(int cv = 0) {
			CV = cv;
		}

		public static string Name {
			get { return "NCE ReadCV Command"; }
		}

		public DCCProgrammingMode ProgrammingMode { get; set; }
		public int CV { get; set; }

		public override IResult Execute(IAdapter adapter) {
			byte command = ProgrammingMode switch {
				DCCProgrammingMode.Direct => 0xA9,
				DCCProgrammingMode.Paged => 0xA1,
				DCCProgrammingMode.Register => 0xA7,
				_ => throw new UnsupportedCommandException("Invalid CV access type provided.")
			};
			return SendAndReceieve(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command));
		}

		public override string ToString() {
			return $"READ CV ({CV}/{ProgrammingMode})";
		}
	}
}