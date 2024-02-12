using DCCRailway.System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Virtual;
using NUnit.Framework;
using VirtualAdapter = DCCRailway.System.Virtual.Adapters.VirtualAdapter;

namespace DCCRailway.Test;

[TestFixture]
public class LoadSystemsTest {
    [Test]
    public void LoadSystemsList() {
        var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null, "Should have at least 1 controller retuned from the GetListOfSystens call");
    }

    /*
    [Test]
    public void InstantiateVirtual() {
        IAdapter? adapter = new VirtualAdapter();
        Assert.That(adapter, Is.TypeOf(typeof(VirtualAdapter)), "Should be a Virtual Controller Created");

        IController? controller;

        controller = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(controller,Is.Not.Null);
        Assert.That(controller,Is.TypeOf(typeof(VirtualController)), "Should be a Virtual:Virtual Controller Created");

        controller = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(controller,Is.Not.Null);
        Assert.That(controller, Is.TypeOf(typeof(VirtualController)), "Should be a :Virtual Controller Created");
    }
    
    [Test]
    public void CheckVirtualCommands() {
        var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null,"Should have valid list of systems");

        var controller = SystemFactory.Create("Virtual", "Virtual", new VirtualAdapter());
        Assert.That(controller, Is.Not.Null,"Should have created a Virtual Controller");

        Console.WriteLine("---------------------------------------------------------------");

        var commandsSupported = controller.SupportedCommands;
        Assert.That(commandsSupported,Is.Not.Null);
        Assert.That(commandsSupported?.Count == 2);

        if (commandsSupported != null) {
            foreach (var cmd in commandsSupported) {
                Console.WriteLine("Command==>" + cmd);
            }
        }
    }
    */
}