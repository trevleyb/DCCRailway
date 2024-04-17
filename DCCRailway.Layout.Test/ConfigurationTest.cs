using System.IO.Ports;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using NUnit.Framework;
using Decoder = DCCRailway.Layout.Configuration.Entities.Layout.Decoder;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ConfigurationTest {

    [Test]
    public void TestEntityRepositoryAddAndStore() {

        // This will either load the file, or will create a new one if it does not exist.
        var config = RailwayConfig.New("TestFile","TestFile","TestSystemWithAll.json");
        Assert.That(config is not null);
        Assert.That(config!.Filename, Is.EqualTo("TestSystemWithAll.json"));

        var accessoryRepository = config.Accessories;
        accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory1", Description = "Test Accessory Description1" });
        accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory2", Description = "Test Accessory Description2" });
        accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory3", Description = "Test Accessory Description3" });

        var blockRepository = config.Blocks;
        blockRepository.AddAsync(new Block { Name = "TestBlock1", Description = "Test Block Description1" });
        blockRepository.AddAsync(new Block { Name = "TestBlock2", Description = "Test Block Description2" });
        blockRepository.AddAsync(new Block { Name = "TestBlock3", Description = "Test Block Description3" });

        var locomotiveRepository = config.Locomotives;
        locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive1", Description = "Test Locomotive Description1" });
        locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive2", Description = "Test Locomotive Description2" });
        locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive3", Description = "Test Locomotive Description3" });

        var sensorRepository = config.Sensors;
        sensorRepository.AddAsync(new Sensor { Name = "TestSensor1", Description = "Test Sensor Description1" });
        sensorRepository.AddAsync(new Sensor { Name = "TestSensor2", Description = "Test Sensor Description2" });
        sensorRepository.AddAsync(new Sensor { Name = "TestSensor3", Description = "Test Sensor Description3" });

        var signalRepository = config.Signals;
        signalRepository.AddAsync(new Signal { Name = "TestSignal1", Description = "Test Signal Description1" });
        signalRepository.AddAsync(new Signal { Name = "TestSignal2", Description = "Test Signal Description2" });
        signalRepository.AddAsync(new Signal { Name = "TestSignal3", Description = "Test Signal Description3" });

        var turnoutRepository = config.Turnouts;
        turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout1", Description = "Test Turnout Description1" });
        turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout2", Description = "Test Turnout Description2" });
        turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout3", Description = "Test Turnout Description3" });

        config.Save("TestSystemWithAll.json");

        // Reload the Data Store we just Saved and then check if it is the same
        var config2 = RailwayConfig.Load("TestSystemWithAll.json");
        Assert.That(config2 is not null);
        Assert.That(config2!.Accessories.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Blocks.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Locomotives.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Sensors.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Signals.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Turnouts.GetAllAsync().Result.Count(), Is.EqualTo(3));
        Assert.That(config2!.Accessories.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestAccessory1"));
        Assert.That(config2!.Blocks.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestBlock1"));
        Assert.That(config2!.Locomotives.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestLocomotive1"));
        Assert.That(config2!.Sensors.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestSensor1"));
        Assert.That(config2!.Signals.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestSignal1"));
        Assert.That(config2!.Turnouts.GetAllAsync().Result.ToArray()[0].Name, Is.EqualTo("TestTurnout1"));
    }

    [Test]
    public void DoNothingTest() { }
}