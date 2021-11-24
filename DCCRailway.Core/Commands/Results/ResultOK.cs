using System;

namespace DCCRailway.Core.Commands {
	public class ResultOK : ResultBase, IResultOK, IResult {
		public ResultOK() : base(Array.Empty<byte>()) { }

		public ResultOK(byte? data = null) : base(data) { }

		public ResultOK(byte[]? data = null) : base(data) { }

		public new bool OK {
			get { return true; }
		}
	}
}