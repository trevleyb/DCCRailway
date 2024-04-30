using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class ConfigurationTest {

    [Test]
    public async Task  TestEntityRepositoryAddAndStore() {

        // This will either load the file, or will create a new one if it does not exist.
        var config = RailwayConfig.New("TestFile","TestFile","TestSystemWithAll.json");
        Assert.That(config is not null);
        Assert.That(config!.Filename, Is.EqualTo("TestSystemWithAll.json"));

        var accessoryRepository = config.Accessories;
        await accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory1", Description = "Test Accessory Description1" });
        await accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory2", Description = "Test Accessory Description2" });
        await accessoryRepository.AddAsync(new Accessory { Name = "TestAccessory3", Description = "Test Accessory Description3" });

        var blockRepository = config.Blocks;
        await blockRepository.AddAsync(new Block { Name = "TestBlock1", Description = "Test Block Description1" });
        await blockRepository.AddAsync(new Block { Name = "TestBlock2", Description = "Test Block Description2" });
        await blockRepository.AddAsync(new Block { Name = "TestBlock3", Description = "Test Block Description3" });

        var locomotiveRepository = config.Locomotives;
        await locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive1", Description = "Test Locomotive Description1" });
        await locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive2", Description = "Test Locomotive Description2" });
        await locomotiveRepository.AddAsync(new Locomotive { Name = "TestLocomotive3", Description = "Test Locomotive Description3" });

        var sensorRepository = config.Sensors;
        await sensorRepository.AddAsync(new Sensor { Name = "TestSensor1", Description = "Test Sensor Description1" });
        await sensorRepository.AddAsync(new Sensor { Name = "TestSensor2", Description = "Test Sensor Description2" });
        await sensorRepository.AddAsync(new Sensor { Name = "TestSensor3", Description = "Test Sensor Description3" });

        var signalRepository = config.Signals;
        await signalRepository.AddAsync(new Signal { Name = "TestSignal1", Description = "Test Signal Description1" });
        await signalRepository.AddAsync(new Signal { Name = "TestSignal2", Description = "Test Signal Description2" });
        await signalRepository.AddAsync(new Signal { Name = "TestSignal3", Description = "Test Signal Description3" });

        var turnoutRepository = config.Turnouts;
        await turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout1", Description = "Test Turnout Description1" });
        await turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout2", Description = "Test Turnout Description2" });
        await turnoutRepository.AddAsync(new Turnout { Name = "TestTurnout3", Description = "Test Turnout Description3" });

        config.Save("TestSystemWithAll.json");

        // Reload the Data Store we just Saved and then check if it is the same
        var config2 = RailwayConfig.Load("TestSystemWithAll.json");
        Assert.That(config2 is not null);
        Assert.That(config2!.Accessories.Count(), Is.EqualTo(3));
        Assert.That(config2!.Blocks.Count(), Is.EqualTo(3));
        Assert.That(config2!.Locomotives.Count(), Is.EqualTo(3));
        Assert.That(config2!.Sensors.Count(), Is.EqualTo(3));
        Assert.That(config2!.Signals.Count(), Is.EqualTo(3));
        Assert.That(config2!.Turnouts.Count(), Is.EqualTo(3));
        Assert.That(config2?.Accessories.First().Value.Name, Is.EqualTo("TestAccessory1"));
        Assert.That(config2?.Blocks.First().Value.Name, Is.EqualTo("TestBlock1"));
        Assert.That(config2?.Locomotives.First().Value.Name, Is.EqualTo("TestLocomotive1"));
        Assert.That(config2?.Sensors.First().Value.Name, Is.EqualTo("TestSensor1"));
        Assert.That(config2?.Signals.First().Value.Name, Is.EqualTo("TestSignal1"));
        Assert.That(config2?.Turnouts.First().Value.Name, Is.EqualTo("TestTurnout1"));
    }

    [Test]
    public void DoNothingTest() { }
}