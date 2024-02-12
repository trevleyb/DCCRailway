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
        Assert.That(systems, Is.Not.Null, "Should have at least 1 system retuned from the GetListOfSystens call");
    }

    /*
    [Test]
    public void InstantiateVirtual() {
        IAdapter? adapter = new VirtualAdapter();
        Assert.That(adapter, Is.TypeOf(typeof(VirtualAdapter)), "Should be a Virtual System Created");

        ISystem? system;

        system = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(system,Is.Not.Null);
        Assert.That(system,Is.TypeOf(typeof(VirtualSystem)), "Should be a Virtual:Virtual System Created");

        system = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(system,Is.Not.Null);
        Assert.That(system, Is.TypeOf(typeof(VirtualSystem)), "Should be a :Virtual System Created");
    }
    
    [Test]
    public void CheckVirtualCommands() {
        var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null,"Should have valid list of systems");

        var system = SystemFactory.Create("Virtual", "Virtual", new VirtualAdapter());
        Assert.That(system, Is.Not.Null,"Should have created a Virtual System");

        Console.WriteLine("---------------------------------------------------------------");

        var commandsSupported = system.SupportedCommands;
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