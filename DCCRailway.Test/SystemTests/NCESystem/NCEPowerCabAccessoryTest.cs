using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Systems.NCE;
using DCCRailway.Systems.NCE.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test {
	[TestClass]
	public class NCEPowerCabAccessoryTest {
		[TestMethod]
		public void TogglePoints() {
			var adapter = new NCEUSBSerial("COM3", 19200);
			Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

			var system = SystemFactory.Create("NCE", "PowerCab", adapter);
			Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
			Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

			if (system != null && system.Adapter != null) {
				if (system.CreateCommand<ICmdAccySetState>() is ICmdAccySetState accyCmd) {
					accyCmd.Address = new DCCAddress(0x01, DCCAddressType.Accessory);
					accyCmd.State = DCCAccessoryState.On;
					system.Execute(accyCmd);
					Thread.Sleep(1000);

					accyCmd.State = DCCAccessoryState.Off;
					system.Execute(accyCmd);
					Thread.Sleep(1000);

					accyCmd.State = DCCAccessoryState.Normal;
					system.Execute(accyCmd);
					Thread.Sleep(1000);

					accyCmd.State = DCCAccessoryState.Reversed;
					system.Execute(accyCmd);
					Thread.Sleep(1000);

					accyCmd.State = DCCAccessoryState.Thrown;
					system.Execute(accyCmd);
					Thread.Sleep(1000);

					accyCmd.State = DCCAccessoryState.Closed;
					system.Execute(accyCmd);
					Thread.Sleep(1000);
				}
			}
		}
	}
}