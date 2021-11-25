using DCCRailway.Core.Utilities;
using Serilog;

namespace DCCRailway {
	public class Startup {
		private static void Main(string[] args) {
			Logger.Log.Debug("Startup Arguments: {0}", args.Length);
		}
	}
}