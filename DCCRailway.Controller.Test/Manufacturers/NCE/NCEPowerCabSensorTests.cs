﻿using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.NCE;
using DCCRailway.Controller.NCE.Actions.Commands;
using DCCRailway.Controller.NCE.Actions.Results;
using DCCRailway.Controller.NCE.Adapters;

namespace DCCRailway.Controller.Test.Manufacturers.NCE;

[TestFixture]
[Ignore("This is a hardware test")]
public class NCEPowerCabSensorTest {
    [Test]
    public void TestCabConversion() {
        Assert.That(CalculateAddress(11, 1) == (11 - 1) * 16 + (1 - 1));
        Assert.That(CalculateCabPin(160) == (11, 1));

        Assert.That(CalculateCabPin(160) == (11, 1));
        Assert.That(CalculateCabPin(161) == (11, 2));
        Assert.That(CalculateCabPin(162) == (11, 3));
        Assert.That(CalculateCabPin(163) == (11, 4));
        Assert.That(CalculateCabPin(164) == (11, 5));
        Assert.That(CalculateCabPin(165) == (11, 6));
        Assert.That(CalculateCabPin(166) == (11, 7));
        Assert.That(CalculateCabPin(167) == (11, 8));
        Assert.That(CalculateCabPin(168) == (11, 9));
        Assert.That(CalculateCabPin(169) == (11, 10));
        Assert.That(CalculateCabPin(170) == (11, 11));
        Assert.That(CalculateCabPin(171) == (11, 12));
        Assert.That(CalculateCabPin(172) == (11, 13));
        Assert.That(CalculateCabPin(173) == (11, 14));
        Assert.That(CalculateCabPin(174) == (11, 15));
        Assert.That(CalculateCabPin(175) == (11, 16));
        Assert.That(CalculateCabPin(176) == (12, 1));
        Assert.That(CalculateCabPin(177) == (12, 2));
        Assert.That(CalculateCabPin(178) == (12, 3));
        Assert.That(CalculateCabPin(179) == (12, 4));
        Assert.That(CalculateCabPin(180) == (12, 5));
    }

    protected internal static (byte cab, byte pin) CalculateCabPin(int address) {
        var pin = address % 16 + 1;

        //var cab = (address - pin) / 16 + 1;
        var cab = address / 16 + 1;

        return ((byte)cab, (byte)pin);
    }

    protected internal static int CalculateAddress(byte cab, byte pin) {
        // Formula (copied from JMRI) is :
        return (cab - 1) * 16 + (pin - 1);
    }

    [Test]
    public void TestTheSensor() {
        var adapter = new NCEUSBSerial(LoggerHelper.DebugLogger);
        adapter.PortName = "COM3";
        adapter.BaudRate = 9600;
        ;
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = new CommandStationFactory(LoggerHelper.DebugLogger).Find("NCEPowerCab")?.Create(adapter);

        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab commandStation created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab CommandStation Created");

        if (system?.Adapter != null) {
            var sensorCmd = system.CreateCommand<ICmdSensorGetState>() as NCESensorGetState;

            var states = new byte[2];
            var count  = 0;

            while (count++ < 5) {
                for (byte part = 0; part < 2; part++)
                for (byte pin = 0; pin < 8; pin++) {
                    sensorCmd?.SetAddressByCabPin(4, (byte)(part * 8 + pin));
                    var state = sensorCmd!.Execute() as NCECmdResultSensorState;
                    states[part] = states[part].SetBit(pin, state?.State == DCCAccessoryState.Occupied ? true : false);
                }

                var dumpLine = states[0].FormatBits() + " " + states[1].FormatBits();
                Console.WriteLine("SENSOR STATES: " + dumpLine);
                Thread.Sleep(1000);
            }
        }
    }
}