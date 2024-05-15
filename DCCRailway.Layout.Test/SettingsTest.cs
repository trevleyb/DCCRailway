using DCCRailway.Layout.Entities;
using DCCRailway.Railway;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class SettingsTest {
    [Test]
    public async Task TestEntityRepositoryAddAndStore() {
        // This will either load the file, or will create a new one if it does not exist.
        var config = RailwayManager.New("TestFile", "./TestConfig");

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

        config.Save();

        var config2 = RailwayManager.Load("TestSystemWithAll.json");
        Assert.That(config2, Is.Not.Null);

        var accs = config2.Accessories.GetAll();
        var blks = config2.Blocks.GetAll();
        var locs = config2.Locomotives.GetAll();
        var sens = config2.Sensors.GetAll();
        var sigs = config2.Signals.GetAll();
        var turs = config2.Turnouts.GetAll();

        Assert.That(accs.Count(), Is.EqualTo(3));
        Assert.That(blks.Count(), Is.EqualTo(3));
        Assert.That(locs.Count(), Is.EqualTo(3));
        Assert.That(sens.Count(), Is.EqualTo(3));
        Assert.That(sigs.Count(), Is.EqualTo(3));
        Assert.That(turs.Count(), Is.EqualTo(3));

        var acc = accs.First(x => x.Name.Contains("1")).Name;
        var blk = blks.First(x => x.Name.Contains("1")).Name;
        var loc = locs.First(x => x.Name.Contains("1")).Name;
        var sen = sens.First(x => x.Name.Contains("1")).Name;
        var sig = sigs.First(x => x.Name.Contains("1")).Name;
        var tur = turs.First(x => x.Name.Contains("1")).Name;

        Assert.That(acc, Is.EqualTo("TestAccessory1"));
        Assert.That(blk, Is.EqualTo("TestBlock1"));
        Assert.That(loc, Is.EqualTo("TestLocomotive1"));
        Assert.That(sen, Is.EqualTo("TestSensor1"));
        Assert.That(sig, Is.EqualTo("TestSignal1"));
        Assert.That(tur, Is.EqualTo("TestTurnout1"));
    }

    [Test]
    public void DoNothingTest() { }
}