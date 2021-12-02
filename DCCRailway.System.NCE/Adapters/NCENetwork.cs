using DCCRailway.Core.Systems.Adapters;

namespace DCCRailway.Systems.NCE.Adapters {
	public class NCENetwork : NetworkAdapter, IAdapter {
		public new static string Name {
			get { return "NCE Network Adapter"; }
		}

		public override string Description {
			get { return "NCE-Network"; }
		}
	}
}