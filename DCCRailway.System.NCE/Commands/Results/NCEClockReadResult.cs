using System;
using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEClockReadResult : ResultOK, IResult {
		private readonly int _hour;
		private readonly int _min;

		public NCEClockReadResult(byte[]? data) {
			if (data == null || data.Length != 2) throw new ApplicationException("Invalid data provided to create a Version");
			_hour = data[0];
			_min = data[1];
		}

		public string FastClock {
			get { return $"{_hour:D2}:{_min:D2}"; }
		}
	}
}