using DCCRailway.CmdStation.Adapters;
using DCCRailway.CmdStation.Controllers;

namespace DCCRailway.System.Test;

[TestFixture, Ignore("Re-writing this")]
public class VirtualControllerTest {
    [Test]
    public void TestRegisteredCommands() {
        var systems = new ControllerFactory().Controllers;

        //var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null);
        Assert.That(systems.Count > 0);

        var virtualSystem = new ControllerFactory().Find("Virtual")?.Create(new ConsoleAdapter());

        //var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        var supportedAdapters = virtualSystem?.Adapters;
        Assert.That(supportedAdapters!.Count == 1);

        // todo: Assert.IsTrue(supportedAdapters[0].name == VirtualAdapter.Name);

        var supportedCommands = virtualSystem?.Commands;
        Assert.That(supportedCommands!.Count == 0, " Should not return any since we have not attached an adapter");

        if (virtualSystem != null) {
            virtualSystem.Adapter = virtualSystem.CreateAdapter("Virtual");
            supportedCommands     = virtualSystem.Commands;
        }

        Assert.That(supportedCommands!.Count == 2);
    }

    [Test]
    public void TestRegisteredAndAttach1() {
        var systems = new ControllerFactory().Controllers;

        //var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null);
        Assert.That(systems.Count > 0);

        var virtualSystem = new ControllerFactory().Find("Virtual")?.Create(new ConsoleAdapter());

        //var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        var supportedAdapters = virtualSystem?.Adapters;
        Assert.That(supportedAdapters!.Count == 1);

        if (virtualSystem != null) {
            virtualSystem.Adapter = virtualSystem.CreateAdapter(supportedAdapters[0].Name);
            Assert.That(virtualSystem.Adapter, Is.Not.Null);
        }
    }

    [Test]
    public void TestRegisteredAndAttach2() {
        var systems = new ControllerFactory().Controllers;

        //var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null);
        Assert.That(systems.Count > 0);

        var virtualSystem = new ControllerFactory().Find("Virtual")?.Create(new ConsoleAdapter());

        //var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        var supportedAdapters = virtualSystem?.Adapters;
        Assert.That(supportedAdapters!.Count == 1);

        if (virtualSystem != null) {
            virtualSystem.Adapter = virtualSystem.CreateAdapter("Virtual");
            Assert.That(virtualSystem.Adapter, Is.Not.Null);
        }
    }

    [Test]
    public void TestCommandSupported() {
        var systems = new ControllerFactory().Controllers;

        //var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null);
        Assert.That(systems.Count > 0);

        var virtualSystem = new ControllerFactory().Find("Virtual")?.Create(new ConsoleAdapter());

        //var virtualSystem = SystemFactory.Create("Virtual", "Virtual");
        Assert.That(virtualSystem, Is.Not.Null);
        if (virtualSystem != null) {
            virtualSystem.Adapter = virtualSystem.CreateAdapter("Virtual");
            Assert.That(virtualSystem.Adapter, Is.Not.Null);
        }
    }

    /*
    [Test]
    public void LoadAndCallVirtualSystem() {
        // Create the Adapter and an instance of the Controller
        // ------------------------------------------------------------------------------------
        var adapter = new VirtualAdapter();
        Assert.That(adapter,Is.Not.Null);
        var controller = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(controller,Is.Not.Null);
        Assert.That(controller,Is.TypeOf(typeof(VirtualController)), "Should be a Virtual:Virtual Controller Created");

        // Setup some event management
        // --------------------------------------------

        var dataSent = false;
        var dataRecv = false;

        if (controller != null && controller.Adapter != null) {
            controller.Adapter.DataSent += delegate { dataSent = true; };

            controller.Adapter.DataReceived += delegate { dataRecv = true; };

            adapter.SendData("DUMMY_COMMAND".ToByteArray());
            var dummy_data = adapter.RecvData();
            Assert.That(dummy_data, Is.Null,  "should not have recieved data on a DUMMY Command");
            Assert.That(dataSent, "Should have raised an event that we sent some data.");
            dataSent = false;
            dataRecv = false;

            adapter.SendData("STATUS_COMMAND".ToByteArray());
            var data = adapter.RecvData();
            Console.WriteLine("Got back: '" + data.ToDisplayValues() + "' from the virtual adapter.");
            Console.WriteLine("Size of data returned: " + data?.Length);
            Assert.That(data != null && data.Length == 1 && data[0] == 0x09, "Should have recieved 0x09");
            Assert.That(dataSent, "Should have raised an event that we sent some data.");
            Assert.That(dataRecv, "Should have raised an event that we recv some data.");
        }
    }
    */
}