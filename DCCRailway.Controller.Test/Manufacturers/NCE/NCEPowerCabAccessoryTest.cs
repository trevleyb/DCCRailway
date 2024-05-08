using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.NCE;
using DCCRailway.Controller.NCE.Adapters;

namespace DCCRailway.System.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabAccessoryTest {
    [Test]
    public void TogglePoints() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = new CommandStationFactory().Find("NCEPowerCab")?.Create(adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab commandStation created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab CommandStation Created");

        if (system != null && system.Adapter != null)
            if (system.CreateCommand<ICmdAccySetState>() is ICmdAccySetState accyCmd) {
                accyCmd.Address = new DCCAddress(0x01, DCCAddressType.Accessory);
                accyCmd.State   = DCCAccessoryState.On;
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