using System;
using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Core.Commands {
	public class ResultError : ResultBase, IResultError, IResult {
		public ResultError(string description) : base(Array.Empty<byte>()) {
			Error = description;
		}

		public ResultError(string description, byte[] data) : base(data) {
			Error = description;
		}

		public string Error { get; }

		public new bool OK {
			get { return false; }
		}
	}
}