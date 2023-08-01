using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test;

[TestClass]
public class NCEPowerCabSignalTest {
    [TestMethod]
    public void CycleSignals() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        if (system != null && system.Adapter != null) {
            var signalCmd = system.CreateCommand<ICmdSignalSetAspect>();

            if (signalCmd != null) {
                for (byte i = 0; i < 16; i++) {
                    signalCmd.Aspect = i;

                    for (var sig = 11; sig < 15; sig++) {
                        signalCmd.Address = new DCCAddress(sig, DCCAddressType.Signal);
                        system.Execute(signalCmd);
                        Thread.Sleep(50);
                    }

                    Thread.Sleep(500);
                }

                for (var sig = 11; sig < 15; sig++) {
                    signalCmd.Address = new DCCAddress(sig, DCCAddressType.Signal);
                    signalCmd.Aspect = 30;
                    system.Execute(signalCmd);
                    Thread.Sleep(50);
                }

                Thread.Sleep(500);

                for (var sig = 11; sig < 15; sig++) {
                    signalCmd.Address = new DCCAddress(sig, DCCAddressType.Signal);
                    signalCmd.Off = true;
                    system.Execute(signalCmd);
                    Thread.Sleep(50);
                }

                Thread.Sleep(500);
            }
        }
    }
}