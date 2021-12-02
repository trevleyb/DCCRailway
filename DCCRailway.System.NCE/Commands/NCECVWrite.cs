using DCCRailway.Core.Common;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCECVWrite : NCECommandBase, ICmdCVWrite, ICommand {
		public NCECVWrite(int cv = 0, byte value = 0) {
			CV = cv;
			Value = value;
		}

		public static string Name {
			get { return "NCE WriteCV Command"; }
		}

		public DCCProgrammingMode ProgrammingMode { get; set; }
		public int CV { get; set; }
		public byte Value { get; set; }

		public override IResult Execute(IAdapter adapter) {
			byte command = ProgrammingMode switch {
				DCCProgrammingMode.Direct => 0xA8,
				DCCProgrammingMode.Paged => 0xA0,
				DCCProgrammingMode.Register => 0xA6,
				_ => throw new UnsupportedCommandException("Invalid CV access type provided.")
			};
			return SendAndReceieve(adapter, new NCEDataReadValidation(), CV.ToByteArray().AddToArray(command).AddToArray(Value));
		}

		public override string ToString() {
			return $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
		}
	}
}