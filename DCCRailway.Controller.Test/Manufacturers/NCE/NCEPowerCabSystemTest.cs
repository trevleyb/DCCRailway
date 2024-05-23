using DCCRailway.Common.Types;
using DCCRailway.Controller.Adapters;

namespace DCCRailway.Controller.Test.Manufacturers.NCE;

[TestFixture]
[Ignore("This is a hardware test")]
public class NCEPowerCab {
    [Test]
    public void GetSerialList() {
        var portList = SerialAdapter.PortNames;

        foreach (var port in portList) Console.WriteLine("Port==> " + port);

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

        // Create the Adapter and an instance of the CommandStation
        // ------------------------------------------------------------------------------------
        //var adapter = new SerialAdapter("cu.SLAB_USBtoUART",19200);
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var commandStation = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(commandStation, "Should have an NCE PowerCab commandStation created.");
        Assert.IsInstanceOfType(commandStation, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab CommandStation Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (commandStation != null && commandStation.Adapter != null) {
            commandStation.Adapter.DataSent += delegate { dataSent = true; };

            commandStation.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = commandStation.CreateCommand<IDummyCmd>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(IDummyCmd), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEDummyCmd), "Should in fact be a NCE Dummy");

            var result = commandStation.Execute(dummyCmd);
            Assert.IsNotNull(result, "Should have recieved a resultOld of some description");
            Assert.IsInstanceOfType(result, typeof(ICommandResult), "Should be an IResultOld type");
            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }

    [Test]
    public void CheckVersionStatus() {
        // Create the Adapter and an instance of the CommandStation
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var commandStation = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(commandStation, "Should have an NCE PowerCab commandStation created.");
        Assert.IsInstanceOfType(commandStation, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab CommandStation Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (commandStation != null && commandStation.Adapter != null) {
            commandStation.Adapter.DataSent += delegate { dataSent = true; };

            commandStation.Adapter.DataReceived += delegate { dataRecv = true; };

            var dummyCmd = commandStation.CreateCommand<ICmdStatus>();
            Assert.IsNotNull(dummyCmd);
            Assert.IsInstanceOfType(dummyCmd, typeof(ICmdStatus), "Should be type IDummy");
            Assert.IsInstanceOfType(dummyCmd, typeof(NCEStatusCmd), "Should in fact be a NCE Dummy");

            var result = commandStation.Execute(dummyCmd);
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
        // Create the Adapter and an instance of the CommandStation
        // ------------------------------------------------------------------------------------
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");

        var commandStation = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(commandStation, "Should have an NCE PowerCab commandStation created.");
        Assert.IsInstanceOfType(commandStation, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab CommandStation Created");

        // Set the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var setTime = commandStation.CreateCommand<ICmdClockSet>() as NCEClockSet;
        Assert.IsNotNull(setTime);
        setTime.Hour     = 9;
        setTime.Minute   = 30;
        setTime.Is24Hour = false;
        setTime.Ratio    = 15; // Fast, 1 minute = 15 seconds
        var setTimeRes = commandStation.Execute(setTime);
        Assert.IsInstanceOfType(setTimeRes, typeof(ResultOldOk));

        // Read the time on the NCE PowerCab
        // --------------------------------------------------------------------------
        var getTime = commandStation.CreateCommand<ICmdClockRead>() as NCEClockRead;
        Assert.IsNotNull(getTime);
        var getTimeRes = commandStation.Execute(getTime) as NCEClockReadResultOld;
        Assert.IsInstanceOfType(getTimeRes, typeof(IResultOld));
        Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResultOld));
        Assert.IsTrue(getTimeRes?.FastClock == "09:30");

        var startClock = commandStation.CreateCommand<ICmdClockStart>();
        Assert.IsNotNull(startClock);
        var startClockRes = commandStation.Execute(startClock);
        Assert.IsNotNull(startClockRes);
        Assert.IsInstanceOfType(startClockRes, typeof(IResultOld));
        Assert.IsInstanceOfType(startClockRes, typeof(ResultOldOk));

        for (var i = 0; i < 30; i++) {
            // Read the time on the NCE PowerCab
            // --------------------------------------------------------------------------
            var getTimeLoop = commandStation.CreateCommand<ICmdClockRead>() as NCEClockRead;
            Assert.IsNotNull(getTimeLoop);
            getTimeRes = commandStation.Execute(getTime) as NCEClockReadResultOld;

            if (getTimeRes != null) {
                Assert.IsInstanceOfType(getTimeRes, typeof(IResultOld));
                Assert.IsInstanceOfType(getTimeRes, typeof(NCEClockReadResultOld));
                var fastClock = getTimeRes.FastClock;
                Console.WriteLine(fastClock);
            }

            Thread.Sleep(500);
        }

        var stopClock = commandStation.CreateCommand<ICmdClockStop>();
        Assert.IsNotNull(startClock);

        if (stopClock != null) {
            var stopClockRes = commandStation.Execute(stopClock);
            Assert.IsNotNull(stopClockRes);
            Assert.IsInstanceOfType(startClockRes, typeof(IResultOld));
            Assert.IsInstanceOfType(startClockRes, typeof(ResultOldOk));
        }
    }
    */
}