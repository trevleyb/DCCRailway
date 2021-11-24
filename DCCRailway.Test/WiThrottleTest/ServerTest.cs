using DCCRailway.Server.WiThrottle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.WiThrottleTest {
	[TestClass]
	public class ServerTest {
		[TestMethod]
		public void RunServer() {
			var t = new Thread(delegate() {
				WiThrottleServer myserver = new();
			});
			t.Start();
			Console.WriteLine("Server Started...!");
		}
	}
}