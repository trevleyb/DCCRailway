using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Systems.NCE;
using DCCRailway.Systems.NCE.Adapters;
using DCCRailway.Systems.NCE.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test; 

[TestClass]
public class NCEPowerCab {
    [TestMethod]
    public void GetSerialList() {
        var portList = SerialAdapter.PortNames;
        foreach (var port in portList) Console.WriteLine("Port==> " + port);

        //Assert.Fail("Stop here to see the output");
    }

    [TestMethod]
    public void TestAddressRange() {
        DCCAddress address = new(3, DCCAddressType.Short);
        Assert.IsTrue(address.Address == 3);
        Assert.IsTrue(address.AddressType == DCCAddressType.Short);
        Assert.IsTrue(address.LowAddress == 3);
        Assert.IsTrue(address.HighAddress == 0);

        address.Address = 127;
        Assert.IsTrue(address.Address == 127);
        Assert.IsTrue(address.AddressType == DCCAddressType.Short);
        Assert.IsTrue(address.LowAddress == 127);
        Assert.IsTrue(address.HighAddress == 0);

        address.AddressType = DCCAddressType.Long;
        Assert.IsTrue(address.Address == 127);
        Assert.IsTrue(address.AddressType == DCCAddressType.Long);
        Assert.IsTrue(address.LowAddress == 127);
        Assert.IsTrue(address.HighAddress == 0xC0);

        // 1029 = 00000100(4) 00000101(5)
        address.AddressType = DCCAddressType.Short;
        address.Address = 1029;
        Assert.IsTrue(address.Address == 1029);
        Assert.IsTrue(address.AddressType == DCCAddressType.Long);
        Assert.IsTrue(address.LowAddress == 5);
        Assert.IsTrue(address.HighAddress == 196);

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => address.Address = 0);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => address.Address = 10001);
    }

    [TestMethod]
    public void CheckDummyStatus() {
        var systems = SystemFactory.SupportedSystems();

        // Create the Adapter and an instance of the System
        // ------------------------------------------------------------------------------------
        //var adapter = new SerialAdapter("cu.SLAB_USBtoUART",19200);
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;
        if (system != null && system.Adapter != null) {
            system.Adapter.DataSent += delegate { dataSent = true; };

            system.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = system.CreateCommand<IDummyCmd>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(IDummyCmd), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEDummyCmd), "Should in fact be a NCE Dummy");

            var result = system.Execute(dummyCmd);
            Assert.IsNotNull(result, "Should have recieved a result of some description");
            Assert.IsInstanceOfType(result, typeof(IResult), "Should be an IResult type");
            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }

    [TestMethod]
    public void CheckVersionStatus() {
        // Create the Adapter and an instance of the System
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (system != null && system.Adapter != null) {
            system.Adapter.DataSent += delegate { dataSent = true; };

            system.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = system.CreateCommand<ICmdStatus>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(ICmdStatus), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEStatusCmd), "Should in fact be a NCE Dummy");

            var result = system.Execute(dummyCmd);
            Assert.IsInstanceOfType(result, typeof(IResult), "Should be an IResult type");
            Assert.IsNotNull(result, "Should have recieved a result of some description");
            Assert.IsInstanceOfType(result, typeof(IResultStatus), "Should in fact be a IResult Status type");
            Assert.IsInstanceOfType(result, typeof(NCEStatusResult), "Should in fact be a specified NCE Result Status type");
            Assert.IsTrue(result.OK, "Result should be OK");
            Assert.IsTrue(!string.IsNullOrEmpty(((NCEStatusResult) result).Version), "Result Version number should not be null or empty");
            Console.WriteLine("Status=>" + ((NCEStatusResult) result).Version);

            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }

    [TestMethod]
    public void TestClockFunctions() {
        // Create the Adapter and an instance of the System
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        // Set the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var setTime = system.CreateCommand<ICmdClockSet>() as NCESetClock;
        Assert.IsNotNull(setTime);
        setTime.Hour = 9;
        setTime.Minute = 30;
        setTime.Is24Hour = false;
        setTime.Ratio = 15; // Fast, 1 minute = 15 seconds 
        var setTimeRes = system.Execute(setTime);
        Assert.IsInstanceOfType(setTimeRes, typeof(ResultOK));

        // Read the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var getTime = system.CreateCommand<ICmdClockRead>() as NCEReadClock;
        Assert.IsNotNull(getTime);
        var getTimeRes = system.Execute(getTime) as NCEClockReadResult;
        Assert.IsInstanceOfType(getTimeRes, typeof(IResult));
        Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResult));
        Assert.IsTrue(getTimeRes?.FastClock == "09:30");

        var startClock = system.CreateCommand<ICmdClockStart>();
        Assert.IsNotNull(startClock);
        var startClockRes = system.Execute(startClock);
        Assert.IsNotNull(startClockRes);
        Assert.IsInstanceOfType(startClockRes, typeof(IResult));
        Assert.IsInstanceOfType(startClockRes, typeof(ResultOK));

        for (var i = 0; i < 30; i++) {
            // Read the time on the NCE PowerCab
            // --------------------------------------------------------------------------
            var getTimeLoop = system.CreateCommand<ICmdClockRead>() as NCEReadClock;
            Assert.IsNotNull(getTimeLoop);
            getTimeRes = system.Execute(getTime) as NCEClockReadResult;
            if (getTimeRes != null) {
                Assert.IsInstanceOfType(getTimeRes, typeof(IResult));
                Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResult));
                var fastClock = getTimeRes.FastClock;
                Console.WriteLine(fastClock);
            }

            Thread.Sleep(500);
        }

        var stopClock = system.CreateCommand<ICmdClockStop>();
        Assert.IsNotNull(startClock);
        if (stopClock != null) {
            var stopClockRes = system.Execute(stopClock);
            Assert.IsNotNull(stopClockRes);
            Assert.IsInstanceOfType(startClockRes, typeof(IResult));
            Assert.IsInstanceOfType(startClockRes, typeof(ResultOK));
        }
    }
}