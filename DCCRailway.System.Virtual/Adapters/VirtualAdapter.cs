using System;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Events;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.Virtual {
	public class VirtualAdapter : BaseAdapter, IAdapter {
		private byte[] _lastCommand;

		public static string Name {
			get { return "Virtual Adapter"; }
		}

		public override string Description {
			get { return "Virtual"; }
		}

		public bool IsConnected { get; private set; }

		public void Connect() {
			Console.WriteLine("Connecting to the Virtual Adapter");
			IsConnected = true;
		}

		public void Disconnect() {
			Console.WriteLine("Disconnecting from the Virtual Adapter");
			IsConnected = false;
		}

		public byte[]? RecvData(ICommand? command = null) {
			Console.WriteLine("Listening for data from the Adapter: '" + _lastCommand.FromByteArray() + "'");
			var result = _lastCommand.FromByteArray() switch {
				"STATUS_COMMAND" => "OK".ToByteArray(),
				"DUMMY_COMMAND" => "OK".ToByteArray(),
				_ => null
			};
			Console.WriteLine("Data to return: '" + result.FromByteArray() + "'");
			OnDataRecieved(new DataRecvArgs(result, this, command));
			return result;
		}

		public void SendData(byte[] data, ICommand? command = null) {
			Console.WriteLine("Sending data to the Adapter");
			_lastCommand = data;
			Console.WriteLine("Data Sent: " + data.FromByteArray());
			OnDataSent(new DataSentArgs(data, this, command));
		}
	}
}