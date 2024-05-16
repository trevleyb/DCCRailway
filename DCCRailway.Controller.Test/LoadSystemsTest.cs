using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Test;

[TestFixture]
public class LoadSystemsTest {
    [Test]
    public void LoadSystemsList() {
        var systems = new CommandStationFactory(LoggerHelper.ConsoleLogger).Controllers;
        Assert.That(systems, Is.Not.Null, "Should have at least 1 commandStation returned from the Controllers call");
    }

    /*
    [Test]
    public void InstantiateVirtual() {
        IAdapter? adapter = new VirtualAdapter();
        Assert.That(adapter, Is.TypeOf(typeof(VirtualAdapter)), "Should be a Virtual CommandStation Created");

        ICommandStation? commandStation;

        commandStation = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(commandStation,Is.Not.Null);
        Assert.That(commandStation,Is.TypeOf(typeof(VirtualCommandStation)), "Should be a Virtual:Virtual CommandStation Created");

        commandStation = SystemFactory.Create("Virtual", "Virtual", adapter);
        Assert.That(commandStation,Is.Not.Null);
        Assert.That(commandStation, Is.TypeOf(typeof(VirtualCommandStation)), "Should be a :Virtual CommandStation Created");
    }

    [Test]
    public void CheckVirtualCommands() {
        var systems = SystemFactory.SupportedSystems();
        Assert.That(systems, Is.Not.Null,"Should have valid list of systems");

        var commandStation = SystemFactory.Create("Virtual", "Virtual", new VirtualAdapter());
        Assert.That(commandStation, Is.Not.Null,"Should have created a Virtual CommandStation");

        Console.WriteLine("---------------------------------------------------------------");

        var commandsSupported = commandStation.SupportedCommands;
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