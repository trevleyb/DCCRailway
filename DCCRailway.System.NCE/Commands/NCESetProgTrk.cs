﻿using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCESetProgTrk : NCECommandBase, ICmdTrackProg {
		public static string Name {
			get { return "NCE Switch to the Programming Track"; }
		}

		public override IResult Execute(IAdapter adapter) {
			return SendAndReceieve(adapter, new NCEStandardValidation(), 0x9E);
		}

		public override string ToString() {
			return "PROGRAMMING TRACK";
		}
	}
}