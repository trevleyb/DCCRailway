using System;

namespace DCCRailway.Core.Systems.Commands.Results {
	public class ResultBase : IResult {
		public ResultBase(byte[]? data = null) {
			Data = data;
		}

		public ResultBase(byte? data = null) {
			Data = data == null ? Array.Empty<byte>() : new[] { (byte)data };
		}

		public byte[]? Data { get; }

		public int Bytes {
			get { return Data?.Length ?? 0; }
		}

		public byte? Value {
			get { return Data?[0]; }
		}

		public bool OK {
			get { return true; }
		}
	}
}