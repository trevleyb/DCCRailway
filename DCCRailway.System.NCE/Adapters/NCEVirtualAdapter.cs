using System;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Adapters {
	public class NCEVirtualAdapter : VirtualAdapter, IAdapter {
		public override string Description {
			get { return "NCE Virtual Adapter"; }
		}

		protected override byte[]? MapSimulatorResult(object? lastResult, ICommand command) {
			if (lastResult == null) return Array.Empty<byte>();
			return ((string)lastResult!).ToByteArray() ?? Array.Empty<byte>();
		}
	}
}