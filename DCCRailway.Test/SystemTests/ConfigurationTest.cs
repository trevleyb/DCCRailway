using System.IO.Ports;
using DCCRailway.Configuration;
using DCCRailway.Configuration.Conversion.JMRI;
using DCCRailway.System.Types;
using NUnit.Framework;
using Decoder = DCCRailway.Configuration.Decoder;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ConfigurationTest {

    [Test]
    public void SaveSystemConfigTestSimple() {

        var system = new Configuration.System {
            Name        = "TestSystem",
            Description = "Test System Description"
        };

        system.Save("SaveSystemConfigTestSimple.json");
        
        var restore = Configuration.System.Load("SaveSystemConfigTestSimple.json");
        Assert.That(restore is not null);
        Assert.That(restore!.Name, Is.EqualTo(system.Name));
        Assert.That(restore!.Description, Is.EqualTo(system.Description));
    }
    
    [Test]
    public void SaveSystemConfigWithParameters() {

        var system = new Configuration.System {
            Name        = "TestSystem",
            Description = "Test System Description"
        };
        system.Controllers.Add(new Controller { Name = "TestController", Description = "Test Controller Description" });
        system.Parameters.Set("style", "steam");
        system.Parameters.Set("length", 27);
        system.Parameters.Set("cv", 127);
        system.Parameters.Set("parity", Parity.Odd);
        
        system.Save("SaveSystemConfigWithParameters.json");
        
        var restore = Configuration.System.Load("SaveSystemConfigWithParameters.json");
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
    public void SaveConfigFileTest() {
        //Configuration.Configuration config = new() { Name = @"testconfig.xml" };
        //TO-DO: config!.Systems.Add(new Controller.Config.Controller("System1"));

        //var adapter    = new Adapter { Name = "NCEUSB" };
        //var parameters = new Parameters();

        //parameters.Set<string>("style", "steam");
        //parameters.Set<long>("length", 27);
        //parameters.Set<byte>("cv", 127);
        //parameters.Set("parity", Parity.Odd);
        //parameters.Set("Address", new DCCAddress(3076));

        //adapter.Parameters = parameters;

        //config!.Systems!.Find(x => x.Name == "System1")!.Adapter = adapter;

        //var decoder = new Decoder { Address = 1029, AddressType = DCCAddressType.Long, Protocol = DCCProtocol.DCC28 };

        //config.Accessories.Add(new Accessory { Name = "Accessory1", Description = "Accessory Description", Decoder = decoder, Parameters = parameters });
        //config.Signals.Add(new Signal { Name        = "Signal1", Description    = "Loco Description", Decoder      = decoder, Parameters = parameters });
        //config.Sensors.Add(new Sensor { Name        = "Sensor1", Description    = "Loco Description", Decoder      = decoder, Parameters = parameters });
        //config.Turnouts.Add(new Turnout { Name      = "Turnout1", Description   = "Loco Description", Decoder      = decoder, Parameters = parameters });
        //config.Blocks.Add(new Block { Name          = "Block1", Description     = "Loco Description" });

        //config.Locos = JMRIRosterImporter.Import("roster.xml");

        //config.Save();

        //var loadConfig = Configuration.Configuration.Load(config.Name);
        //Assert.That(loadConfig, Is.Not.Null);
        //Assert.That(loadConfig, Is.EqualTo(config));
    }

    //[Test]
    //public void ManufacturersTest() {
    //    var mnf = new Manufacturers();
    //    Assert.That(mnf.Count == 169);
    //}

    [Test]
    public void DoNothingTest() { }
}