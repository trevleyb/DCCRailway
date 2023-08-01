using DCCRailway.System;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Utilities;
using DCCRailway.System.Virtual;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test;

[TestClass]
public class VirtualSystemTest {
    [TestMethod]
    public void TestRegisteredCommands() {
        var systems = SystemFactory.SupportedSystems();
        Assert.IsNotNull(systems);
        Assert.IsTrue(systems.Count > 0);

        var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.IsNotNull(virtualSystem);

        var supportedAdapters = virtualSystem.SupportedAdapters;
        Assert.IsTrue(supportedAdapters!.Count == 1);
        // todo: Assert.IsTrue(supportedAdapters[0].name == VirtualAdapter.Name);

        var supportedCommands = virtualSystem.SupportedCommands;
        Assert.IsTrue(supportedCommands!.Count == 0, " Should not return any since we have not attached an adapter");

        virtualSystem.Adapter = virtualSystem.CreateAdapter<VirtualAdapter>();
        supportedCommands = virtualSystem.SupportedCommands;
        Assert.IsTrue(supportedCommands!.Count == 2);
    }

    [TestMethod]
    public void TestRegisteredAndAttach1() {
        var systems = SystemFactory.SupportedSystems();
        Assert.IsNotNull(systems);
        Assert.IsTrue(systems.Count > 0);

        var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.IsNotNull(virtualSystem);

        var supportedAdapters = virtualSystem.SupportedAdapters;
        Assert.IsTrue(supportedAdapters!.Count == 1);

        virtualSystem.Adapter = virtualSystem.CreateAdapter(supportedAdapters[0].name);
        Assert.IsNotNull(virtualSystem.Adapter);
    }

    [TestMethod]
    public void TestRegisteredAndAttach2() {
        var systems = SystemFactory.SupportedSystems();
        Assert.IsNotNull(systems);
        Assert.IsTrue(systems.Count > 0);

        var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.IsNotNull(virtualSystem);

        var supportedAdapters = virtualSystem.SupportedAdapters;
        Assert.IsTrue(supportedAdapters!.Count == 1);

        virtualSystem.Adapter = virtualSystem.CreateAdapter<VirtualAdapter>();
        Assert.IsNotNull(virtualSystem.Adapter);
    }

    [TestMethod]
    public void TestCommandSupported() {
        var systems = SystemFactory.SupportedSystems();
        Assert.IsNotNull(systems);
        Assert.IsTrue(systems.Count > 0);

        var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.IsNotNull(virtualSystem);
        virtualSystem.Adapter = virtualSystem.CreateAdapter<VirtualAdapter>();
        Assert.IsNotNull(virtualSystem.Adapter);

        Assert.IsTrue(virtualSystem.IsCommandSupported<IDummyCmd>());
        Assert.IsTrue(virtualSystem.IsCommandSupported<ICmdStatus>());
        Assert.IsFalse(virtualSystem.IsCommandSupported<ICmdClockStart>());
    }

    [TestMethod]
    public void LoadAndCallVirtualSystem() {
        // Create the Adapter and an instance of the System
        // ------------------------------------------------------------------------------------
        var adapter = new VirtualAdapter();
        Assert.IsNotNull(adapter);
        var system = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.IsNotNull(system);
        Assert.IsInstanceOfType(system, typeof(VirtualSystem), "Should be a Virtual:Virtual System Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (system != null && system.Adapter != null) {
            system.Adapter.DataSent += delegate { dataSent = true; };

            system.Adapter.DataReceived += delegate { dataRecv = true; };

            adapter.SendData("DUMMY_COMMAND".ToByteArray());
            var dummy_data = adapter.RecvData();
            Assert.IsNull(dummy_data, "should not have recieved data on a DUMMY Command");
            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            dataSent = false;
            dataRecv = false;

            adapter.SendData("STATUS_COMMAND".ToByteArray());
            var data = adapter.RecvData();
            Console.WriteLine("Got back: '" + data.ToDisplayValues() + "' from the virtual adapter.");
            Console.WriteLine("Size of data returned: " + data?.Length);
            Assert.IsTrue(data != null && data.Length == 1 && data[0] == 0x09, "Should have recieved 0x09");
            Assert.IsTrue(dataSent, "Should have raised an event that we sent some data.");
            Assert.IsTrue(dataRecv, "Should have raised an event that we recv some data.");
        }
    }
}