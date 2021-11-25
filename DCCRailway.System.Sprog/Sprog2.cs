﻿using System;
using System.Collections.Generic;
using DCCRailway.Core;
using DCCRailway.Core.Common;
using DCCRailway.Core.Events;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.Sprog {
	public class Sprog2 : SystemBase, ISystem {
		public static string Manufacturer {
			get { return "Virtual"; }
		}

		public static string SystemName {
			get { return "Virtual"; }
		}

		public override IDCCAddress CreateAddress() {
			return new DCCAddress();
		}

		public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
			return new DCCAddress(address, type);
		}

		public override List<(Type adapter, string name)>? SupportedAdapters {
			get {
				List<(Type adapter, string name)>? adapters = new();

				//adapters.Add ((typeof (VirtualAdapter), VirtualAdapter.Name));
				return adapters;
			}
		}

		protected override void RegisterCommands() {
			//Register<IDummyCmd> (typeof (Commands.VirtualDummy));
			//Register<ICmdStatus> (typeof (Commands.VirtualStatus));
		}

		#region Manage the events from the Adapter
		protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
			Core.Utilities.Logger.Log.Debug("Error occurred in the Adapter: " + e);
		}

		protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
			Core.Utilities.Logger.Log.Debug("A state change event occurred in the Adapter: " + e.EventType);
		}

		protected override void Adapter_DataSent(object? sender, DataSentArgs e) {
			Core.Utilities.Logger.Log.Debug("Data was sent to the Adapter: " + e.Data?.ToDisplayValues());
		}

		protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) {
			Core.Utilities.Logger.Log.Debug("Data was recieved from the Adapter: " + e.Data?.ToDisplayValues());
		}
		#endregion
	}
}