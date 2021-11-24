using System;

namespace DCCRailway.Core.Exceptions {
	public class UnsupportedCommandException : Exception {
		public UnsupportedCommandException(string? message) : base(message) { }
	}
}