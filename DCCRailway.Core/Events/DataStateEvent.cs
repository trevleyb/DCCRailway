using System;
using DCCRailway.Core.Adapters;

namespace DCCRailway.Core.Events {
	public class StateChangedArgs : EventArgs {
		public StateChangedArgs(string eventType, IAdapter? adapter = null) {
			Adapter = adapter;
			EventType = eventType;
		}

		public IAdapter? Adapter { get; set; }
		public string EventType { get; set; }

		public override string ToString() {
			return $"STATE: {Adapter?.Description ?? "Unknown Adapter"}<==>{EventType}";
		}
	}
}