using System;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Events;

namespace DCCRailway.Core.Adapters {
	public interface IAdapter {
		public static string Name { get; }
		public string Description { get; }
		bool IsConnected { get; }

		void Connect();
		void Disconnect();

		void SendData(byte[] data, ICommand command);
		byte[]? RecvData(ICommand command);

		event EventHandler<StateChangedArgs> ConnectionStatusChanged;
		event EventHandler<DataRecvArgs> DataReceived;
		event EventHandler<DataSentArgs> DataSent;
		event EventHandler<ErrorArgs> ErrorOccurred;
	}
}