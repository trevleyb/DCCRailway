using DCCRailway.System;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.NCE.Commands;
using DCCRailway.System.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test;

[TestClass]
public class NCEPowerCabSensorTest {
    [TestMethod]
    public void TestCabConversion() {
        Assert.IsTrue(CalculateAddress(11, 1) == (11 - 1) * 16 + (1 - 1));
        Assert.IsTrue(CalculateCabPin(160) == (11, 1));
    }

    protected internal static (byte cab, byte pin) CalculateCabPin(int address) {
        var pin = address % 16 + 1;
        var cab = (address - pin) / 16 + 1;

        return ((byte)cab, (byte)pin);
    }

    protected internal static int CalculateAddress(byte cab, byte pin) =>

        // Formula (copied from JMRI) is :
        (cab - 1) * 16 + (pin - 1);

    [TestMethod]
    public void TestTheSensor() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        if (system.Adapter != null) {
            var sensorCmd = system.CreateCommand<ICmdSensorGetState>() as NCESensorGetState;

            var states = new byte[2];
            var loop   = true;

            while (loop) {
                for (byte part = 0; part < 2; part++)
                for (byte pin = 0; pin < 8; pin++) {
                    sensorCmd?.SetAddressByCabPin(4, (byte)(part * 8 + pin));
                    var state = system.Execute(sensorCmd!) as NCECommandResultSensorState;
                    states[part] = states[part].SetBit(pin, state?.State ?? false);
                }

                var dumpline = states[0].FormatBits() + " " + states[1].FormatBits();
                Console.WriteLine("SENSOR STATES: " + dumpline);
                Thread.Sleep(1000);
            }
        }
    }
}