using DCCRailway.System;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.Types;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class NCEPowerCabAccessoryTest {
    [Test]
    public void TogglePoints() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab system created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab System Created");

        if (system != null && system.Adapter != null) {
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
}