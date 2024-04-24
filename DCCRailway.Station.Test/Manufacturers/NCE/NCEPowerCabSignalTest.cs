using System.Diagnostics;
using System.IO.Ports;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.NCE;
using DCCRailway.Station.NCE.Adapters;

namespace DCCRailway.System.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabSignalTest {
    [Test]
    public void CycleSignals() {
        //var adapter = new NCEUSBSerial("COM3", 19200);
        var ports   = SerialPort.GetPortNames();
        var adapter = new NCEUSBSerial("/dev/cu.SLAB_USBtoUART", 19200, 8, Parity.None, StopBits.One, 500);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);

        //var system = SystemFactory.Create("NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null) {
            system.ControllerEvent += (sender, args) => { Debug.WriteLine(args.ToString()); };

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
}