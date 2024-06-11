using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.NCE;
using DCCRailway.Controller.NCE.Adapters;

namespace DCCRailway.Controller.Test.Manufacturers.NCE;

[TestFixture]
[Ignore("This is a hardware test")]
public class NCEPowerCabAccessoryTest {
    [Test]
    public void TogglePoints() {
        var adapter = new NCEUSBSerial(LoggerHelper.DebugLogger);
        adapter.PortName = "COM3";
        adapter.BaudRate = 9600;
        ;
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = new CommandStationFactory(LoggerHelper.DebugLogger).Find("NCEPowerCab")?.Create(adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab commandStation created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab CommandStation Created");

        if (system != null && system.Adapter != null) {
            if (system.CreateCommand<ICmdAccySetState>() is ICmdAccySetState accyCmd) {
                accyCmd.Address = new DCCAddress(0x01, DCCAddressType.Accessory);
                accyCmd.State   = DCCAccessoryState.On;
                accyCmd.Execute();
                Thread.Sleep(1000);

                accyCmd.State = DCCAccessoryState.Off;
                accyCmd.Execute();
                Thread.Sleep(1000);

                accyCmd.State = DCCAccessoryState.Normal;
                accyCmd.Execute();
                Thread.Sleep(1000);

                accyCmd.State = DCCAccessoryState.Reversed;
                accyCmd.Execute();
                Thread.Sleep(1000);

                accyCmd.State = DCCAccessoryState.Thrown;
                accyCmd.Execute();
                Thread.Sleep(1000);

                accyCmd.State = DCCAccessoryState.Closed;
                accyCmd.Execute();
                Thread.Sleep(1000);
            }
        }
    }
}