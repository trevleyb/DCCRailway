using System;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Core.Events {
	public class DataRecvArgs : EventArgs {
		public DataRecvArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
			Adapter = adapter;
			Command = command;
			Data = data;
		}

		public ICommand? Command { get; set; }
		public IAdapter? Adapter { get; set; }
		public byte[]? Data { get; }

		public override string ToString() {
			return $"RECVDATA: {Adapter?.Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}<=='{Data.ToDisplayValues()}'";
		}
	}
}