using System;
using DCCRailway.Core.Systems.Commands;

namespace DCCRailway.Core.Systems.Adapters {
	public abstract class NetworkAdapter : BaseAdapter, IAdapter {
		public static string Name {
			get { return "Network Adapter"; }
		}

		public bool IsConnected {
			get { throw new NotImplementedException(); }
		}

		public void Connect() {
			throw new NotImplementedException();
		}

		public void Disconnect() {
			throw new NotImplementedException();
		}

		public byte[]? RecvData(ICommand? command = null) {
			throw new NotImplementedException();
		}

		public void SendData(byte[] data, ICommand? command = null) {
			throw new NotImplementedException();
		}
	}
}