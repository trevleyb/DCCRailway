using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Types;
using DCCRailway.System;
using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.NCE.Commands;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class NCEPowerCab {
    [Test]
    public void GetSerialList() {
        var portList = SerialAdapter.PortNames;

        foreach (var port in portList) {
            Console.WriteLine("Port==> " + port);
        }

        //Assert.Fail("Stop here to see the output");
    }

    [Test]
    public void TestAddressRange() {
        DCCAddress address = new(3, DCCAddressType.Short);
        Assert.That(address.Address == 3);
        Assert.That(address.AddressType == DCCAddressType.Short);
        Assert.That(address.LowAddress == 3);
        Assert.That(address.HighAddress == 0);

        address.Address = 127;
        Assert.That(address.Address == 127);
        Assert.That(address.AddressType == DCCAddressType.Short);
        Assert.That(address.LowAddress == 127);
        Assert.That(address.HighAddress == 0);

        address.AddressType = DCCAddressType.Long;
        Assert.That(address.Address == 127);
        Assert.That(address.AddressType == DCCAddressType.Long);
        Assert.That(address.LowAddress == 127);
        Assert.That(address.HighAddress == 0xC0);

        // 1029 = 00000100(4) 00000101(5)
        address.AddressType = DCCAddressType.Short;
        address.Address     = 1029;
        Assert.That(address.Address == 1029);
        Assert.That(address.AddressType == DCCAddressType.Long);
        Assert.That(address.LowAddress == 5);
        Assert.That(address.HighAddress == 196);

        Assert.Catch<ArgumentOutOfRangeException>(() => address.Address = 0);
        Assert.Catch<ArgumentOutOfRangeException>(() => address.Address = 10001);
    }
/*
    [Test]
    public void CheckDummyStatus() {
        var systems = SystemFactory.SupportedSystems();

        // Create the Adapter and an instance of the Controller
        // ------------------------------------------------------------------------------------
        //var adapter = new SerialAdapter("cu.SLAB_USBtoUART",19200);
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var controller = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(controller, "Should have an NCE PowerCab controller created.");
        Assert.IsInstanceOfType(controller, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab Controller Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (controller != null && controller.Adapter != null) {
            controller.Adapter.DataSent += delegate { dataSent = true; };

            controller.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = controller.CreateCommand<IDummyCmd>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(IDummyCmd), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEDummyCmd), "Should in fact be a NCE Dummy");

            var result = controller.Execute(dummyCmd);
            Assert.IsNotNull(result, "Should have recieved a resultOld of some description");
            Assert.IsInstanceOfType(result, typeof(ICommandResult), "Should be an IResultOld type");
            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }

    [Test]
    public void CheckVersionStatus() {
        // Create the Adapter and an instance of the Controller
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var controller = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(controller, "Should have an NCE PowerCab controller created.");
        Assert.IsInstanceOfType(controller, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab Controller Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (controller != null && controller.Adapter != null) {
            controller.Adapter.DataSent += delegate { dataSent = true; };

            controller.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = controller.CreateCommand<ICmdStatus>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(ICmdStatus), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEStatusCmd), "Should in fact be a NCE Dummy");

            var result = controller.Execute(dummyCmd);
            Assert.IsInstanceOfType(result, typeof(ICommandResult), "Should be an IResultOld type");
            Assert.IsNotNull(result, "Should have recieved a resultOld of some description");
            Assert.IsInstanceOfType(result, typeof(ICommandResult), "Should in fact be a IResultOld Status type");
            Assert.IsInstanceOfType(result, typeof(ICommandResult), "Should in fact be a specified NCE Results Status type");
            Assert.IsTrue(result.OK, "Results should be OK");
            Assert.IsTrue(!string.IsNullOrEmpty(((ICommandResult)result).Version), "Results Version number should not be null or empty");
            Console.WriteLine("Status=>" + ((ICommandResult)result).Version);

            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }

    [Test]
    public void TestClockFunctions() {
        // Create the Adapter and an instance of the Controller
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var controller = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(controller, "Should have an NCE PowerCab controller created.");
        Assert.IsInstanceOfType(controller, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab Controller Created");

        // Set the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var setTime = controller.CreateCommand<ICmdClockSet>() as NCESetClock;
        Assert.IsNotNull(setTime);
        setTime.Hour     = 9;
        setTime.Minute   = 30;
        setTime.Is24Hour = false;
        setTime.Ratio    = 15; // Fast, 1 minute = 15 seconds 
        var setTimeRes = controller.Execute(setTime);
        Assert.IsInstanceOfType(setTimeRes, typeof(ResultOldOk));

        // Read the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var getTime = controller.CreateCommand<ICmdClockRead>() as NCEReadClock;
        Assert.IsNotNull(getTime);
        var getTimeRes = controller.Execute(getTime) as NCEClockReadResultOld;
        Assert.IsInstanceOfType(getTimeRes, typeof(IResultOld));
        Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResultOld));
        Assert.IsTrue(getTimeRes?.FastClock == "09:30");

        var startClock = controller.CreateCommand<ICmdClockStart>();
        Assert.IsNotNull(startClock);
        var startClockRes = controller.Execute(startClock);
        Assert.IsNotNull(startClockRes);
        Assert.IsInstanceOfType(startClockRes, typeof(IResultOld));
        Assert.IsInstanceOfType(startClockRes, typeof(ResultOldOk));

        for (var i = 0; i < 30; i++) {
            // Read the time on the NCE PowerCab
            // --------------------------------------------------------------------------
            var getTimeLoop = controller.CreateCommand<ICmdClockRead>() as NCEReadClock;
            Assert.IsNotNull(getTimeLoop);
            getTimeRes = controller.Execute(getTime) as NCEClockReadResultOld;

            if (getTimeRes != null) {
                Assert.IsInstanceOfType(getTimeRes, typeof(IResultOld));
                Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResultOld));
                var fastClock = getTimeRes.FastClock;
                Console.WriteLine(fastClock);
            }

            Thread.Sleep(500);
        }

        var stopClock = controller.CreateCommand<ICmdClockStop>();
        Assert.IsNotNull(startClock);

        if (stopClock != null) {
            var stopClockRes = controller.Execute(stopClock);
            Assert.IsNotNull(stopClockRes);
            Assert.IsInstanceOfType(startClockRes, typeof(IResultOld));
            Assert.IsInstanceOfType(startClockRes, typeof(ResultOldOk));
        }
    }
    */
}