using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class SettingsTest {
    [Test]
    public void TestEntityRepositoryAddAndStore() {
        // This will either load the file, or will create a new one if it does not exist.
        var config = new RailwaySettings(LoggerHelper.ConsoleLogger).New("./TestConfig", "TestFile");

        var accessoryRepository = config.Accessories;
        accessoryRepository.Add(new Accessory { Name = "TestAccessory1", Description = "Test Accessory Description1" });
        accessoryRepository.Add(new Accessory { Name = "TestAccessory2", Description = "Test Accessory Description2" });
        accessoryRepository.Add(new Accessory { Name = "TestAccessory3", Description = "Test Accessory Description3" });

        var blockRepository = config.Blocks;
        blockRepository.Add(new Block { Name = "TestBlock1", Description = "Test Block Description1" });
        blockRepository.Add(new Block { Name = "TestBlock2", Description = "Test Block Description2" });
        blockRepository.Add(new Block { Name = "TestBlock3", Description = "Test Block Description3" });

        var locomotiveRepository = config.Locomotives;
        locomotiveRepository.Add(new Locomotive
                                     { Name = "TestLocomotive1", Description = "Test Locomotive Description1" });
        locomotiveRepository.Add(new Locomotive
                                     { Name = "TestLocomotive2", Description = "Test Locomotive Description2" });
        locomotiveRepository.Add(new Locomotive
                                     { Name = "TestLocomotive3", Description = "Test Locomotive Description3" });

        var sensorRepository = config.Sensors;
        sensorRepository.Add(new Sensor { Name = "TestSensor1", Description = "Test Sensor Description1" });
        sensorRepository.Add(new Sensor { Name = "TestSensor2", Description = "Test Sensor Description2" });
        sensorRepository.Add(new Sensor { Name = "TestSensor3", Description = "Test Sensor Description3" });

        var signalRepository = config.Signals;
        signalRepository.Add(new Signal { Name = "TestSignal1", Description = "Test Signal Description1" });
        signalRepository.Add(new Signal { Name = "TestSignal2", Description = "Test Signal Description2" });
        signalRepository.Add(new Signal { Name = "TestSignal3", Description = "Test Signal Description3" });

        var turnoutRepository = config.Turnouts;
        turnoutRepository.Add(new Turnout { Name = "TestTurnout1", Description = "Test Turnout Description1" });
        turnoutRepository.Add(new Turnout { Name = "TestTurnout2", Description = "Test Turnout Description2" });
        turnoutRepository.Add(new Turnout { Name = "TestTurnout3", Description = "Test Turnout Description3" });

        config.Save();

        var config2 = new RailwaySettings(LoggerHelper.ConsoleLogger).Load("./TestConfig", "TestFile");
        Assert.That(config2, Is.Not.Null);

        var accs = config2.Accessories.GetAll();
        var blks = config2.Blocks.GetAll();
        var locs = config2.Locomotives.GetAll();
        var sens = config2.Sensors.GetAll();
        var sigs = config2.Signals.GetAll();
        var turs = config2.Turnouts.GetAll();
        var ruts = config2.Routes.GetAll();

        Assert.That(accs.Count(), Is.EqualTo(config.Accessories.Count));
        Assert.That(blks.Count(), Is.EqualTo(config.Blocks.Count));
        Assert.That(locs.Count(), Is.EqualTo(config.Locomotives.Count));
        Assert.That(sens.Count(), Is.EqualTo(config.Sensors.Count));
        Assert.That(sigs.Count(), Is.EqualTo(config.Signals.Count));
        Assert.That(turs.Count(), Is.EqualTo(config.Turnouts.Count));
        Assert.That(ruts.Count(), Is.EqualTo(config.Routes.Count));

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