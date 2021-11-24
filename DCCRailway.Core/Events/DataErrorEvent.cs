using System;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;

namespace DCCRailway.Core.Events {
	public class ErrorArgs : EventArgs {
		public ErrorArgs(string error, IAdapter? adapter = null, ICommand? command = null) {
			Adapter = adapter;
			Command = command;
			Error = error;
		}

		public IAdapter? Adapter { get; set; }
		public ICommand? Command { get; set; }
		public string Error { get; }

		public override string ToString() {
			return $"ERROR: {Adapter?.Description ?? "Unknown Adapter"}:{Command?.ToString() ?? "Unknown Command"}<=={Error}";
		}
	}
}