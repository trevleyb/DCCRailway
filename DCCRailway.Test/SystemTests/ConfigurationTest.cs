using System.IO.Ports;
using DCCRailway.Configuration;
using DCCRailway.Configuration.Conversion.JMRI;
using DCCRailway.Configuration.Entities;
using NUnit.Framework;
using Decoder = DCCRailway.Configuration.Decoder;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ConfigurationTest {

    [Test]
    public void SaveSystemConfigTestSimple() {

        var system = new DCCRailway {
            Name        = "TestSystem",
            Description = "Test System Description"
        };

        system.Save("SaveSystemConfigTestSimple.json");
        
        var restore = DCCRailway.Load("SaveSystemConfigTestSimple.json");
        Assert.That(restore is not null);
        Assert.That(restore!.Name, Is.EqualTo(system.Name));
        Assert.That(restore!.Description, Is.EqualTo(system.Description));
    }
    
    [Test]
    public void SaveSystemConfigWithParameters() {

        var system = new DCCRailway {
            Name        = "TestSystem",
            Description = "Test System Description"
        };
        system.Controllers.Add(new Controller { Name = "TestController", Description = "Test Controller Description" });
        system.Parameters.Set("style", "steam");
        system.Parameters.Set("length", 27);
        system.Parameters.Set("cv", 127);
        system.Parameters.Set("parity", Parity.Odd);
        
        system.Save("SaveSystemConfigWithParameters.json");
        
        var restore = DCCRailway.Load("SaveSystemConfigWithParameters.json");
        Assert.That(restore is not null);
        Assert.That(restore!.Name, Is.EqualTo(system.Name));
        Assert.That(restore!.Description, Is.EqualTo(system.Description));
        Assert.That(restore!.Controllers.Count, Is.EqualTo(1));
        Assert.That(restore!.Parameters.Get<string>("style"), Is.EqualTo("steam"));
        Assert.That(restore!.Parameters.Get<long>("length"), Is.EqualTo(27));
        Assert.That(restore!.Parameters.Get<byte>("cv"), Is.EqualTo(127));
        Assert.That(restore!.Parameters.Get("parity"), Is.EqualTo(Parity.Odd));
    }

    [Test]
    public void SaveConfigFileWithAllOptionsIncluded() {

        var system = new DCCRailway {
            Name        = "TestSystemWithAll",
            Description = "Test System With All"
        };
        system.Controllers.Add(new Controller { Name = "TestController", Description = "Test Controller Description" });
        system.Accessories.Add(new Accessory { Name  = "TestAccessory", Description  = "Test Accessory Description" });
        system.Blocks.Add(new Block { Name           = "TestBlock", Description      = "Test Block Description" });
        system.Locomotives.Add(new Locomotive { Name = "TestLocomotive", Description = "Test Locomotive Description" });
        system.Sensors.Add(new Sensor { Name         = "TestSensor", Description     = "Test Sensor Description" });
        system.Signals.Add(new Signal { Name         = "TestSignal", Description     = "Test Signal Description" });
        system.Turnouts.Add(new Turnout { Name       = "TestTurnout", Description    = "Test Turnout Description" });

        system.Save("TestSystemWithAll.json");
        var restore = DCCRailway.Load("TestSystemWithAll.json");
        
        Assert.That(restore is not null);
        Assert.That(restore!.Name, Is.EqualTo(system.Name));
        Assert.That(restore!.Description, Is.EqualTo(system.Description));
        Assert.That(restore!.Controllers.Count, Is.EqualTo(1));
        Assert.That(restore!.Accessories.Count, Is.EqualTo(1));
        Assert.That(restore!.Blocks.Count, Is.EqualTo(1));
        Assert.That(restore!.Locomotives.Count, Is.EqualTo(1));
        Assert.That(restore!.Sensors.Count, Is.EqualTo(1));
        Assert.That(restore!.Signals.Count, Is.EqualTo(1));
        Assert.That(restore!.Turnouts.Count, Is.EqualTo(1));
        Assert.That(restore!.Accessories[0].Name, Is.EqualTo("TestAccessory"));
        Assert.That(restore!.Blocks[0].Name, Is.EqualTo("TestBlock"));
        Assert.That(restore!.Locomotives[0].Name, Is.EqualTo("TestLocomotive"));
        Assert.That(restore!.Sensors[0].Name, Is.EqualTo("TestSensor"));
        Assert.That(restore!.Signals[0].Name, Is.EqualTo("TestSignal"));
        Assert.That(restore!.Turnouts[0].Name, Is.EqualTo("TestTurnout"));
    }

    [Test]
    public void DoNothingTest() { }
}