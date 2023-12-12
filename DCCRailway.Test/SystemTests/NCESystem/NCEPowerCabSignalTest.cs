using System.Diagnostics;
using System.IO.Ports;
using DCCRailway.System;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test;

[TestClass]
public class NCEPowerCabSignalTest {
    [TestMethod]
    public void CycleSignals() {
        //var adapter = new NCEUSBSerial("COM3", 19200);
        var ports   = SerialPort.GetPortNames();
        var adapter = new NCEUSBSerial("/dev/cu.SLAB_USBtoUART", 19200, 8, Parity.None, StopBits.One, 500);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCEPowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        system.SystemEvent += (sender, args) => {
            Debug.WriteLine(args.ToString());
        };
        
        if (system?.Adapter != null) {
            var signalCmd = system.CreateCommand<ICmdSignalSetAspect>();
            if (signalCmd != null) {
                var aspects = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 30, 31 };
                var signals = new[] { 11, 12, 14, 13 };
                foreach (var aspect in aspects) {
                    foreach (var signal in signals) {
                        signalCmd.Address = new DCCAddress(signal, DCCAddressType.Signal);
                        signalCmd.Aspect  = aspect;
                        system.Execute(signalCmd);
                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}