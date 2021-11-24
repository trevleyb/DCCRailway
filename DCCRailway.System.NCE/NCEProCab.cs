using System;
using System.Collections.Generic;
using DCCRailway.Core;
using DCCRailway.Core.Common;
using DCCRailway.Core.Events;

namespace DCCRailway.Systems.NCE {
	public class NCEProCab : SystemBase, ISystem {
		public override IDCCAddress CreateAddress() {
			return new DCCAddress();
		}

		public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
			return new DCCAddress(address, type);
		}

		public override List<(Type adapter, string name)>? SupportedAdapters {
			get {
				List<(Type adapter, string name)>? adapters = new();

				//adapters.Add((typeof(VirtualAdapter), VirtualAdapter.Name));
				return adapters;
			}
		}

		public static string Manufacturer {
			get { return "NCE"; }
		}

		public static string SystemName {
			get { return "ProCab"; }
		}

		protected override void RegisterCommands() {
			throw new NotImplementedException();
		}

		#region Manage the events from the Adapter
		protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
			throw new NotImplementedException();
		}

		protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
			throw new NotImplementedException();
		}

		protected override void Adapter_DataSent(object? sender, DataSentArgs e) {
			throw new NotImplementedException();
		}

		protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) {
			throw new NotImplementedException();
		}
		#endregion
	}
}