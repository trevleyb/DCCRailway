using System.IO.Ports;
using DCCRailway.Configuration;
using DCCRailway.Configuration.Conversion.JMRI;
using DCCRailway.Configuration.Entities;
using DCCRailway.System.Types;
using NUnit.Framework;
using Decoder = DCCRailway.Configuration.Decoder;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ConfigurationTest {
    [Test]
    public void SaveConfigFileTest() {
        Configuration.Configuration config = new() { Name = @"testconfig.xml" };
        //TO-DO: config!.Systems.Add(new Controller.Config.Controller("System1"));

        var adapter    = new Adapter { Name = "NCEUSB" };
        var parameters = new Parameters();

        parameters.Set<string>("style", "steam");
        parameters.Set<long>("length", 27);
        parameters.Set<byte>("cv", 127);
        parameters.Set("parity", Parity.Odd);
        parameters.Set("Address", new DCCAddress(3076));

        adapter.Parameters = parameters;

        config!.Systems!.Find(x => x.Name == "System1")!.Adapter = adapter;

        var decoder = new Decoder { Address = 1029, AddressType = DCCAddressType.Long, Protocol = DCCProtocol.DCC28 };

        config.Accessories.Add(new Accessory { Name = "Accessory1", Description = "Accessory Description", Decoder = decoder, Parameters = parameters });
        config.Signals.Add(new Signal { Name        = "Signal1", Description    = "Loco Description", Decoder      = decoder, Parameters = parameters });
        config.Sensors.Add(new Sensor { Name        = "Sensor1", Description    = "Loco Description", Decoder      = decoder, Parameters = parameters });
        config.Turnouts.Add(new Turnout { Name      = "Turnout1", Description   = "Loco Description", Decoder      = decoder, Parameters = parameters });
        config.Blocks.Add(new Block { Name          = "Block1", Description     = "Loco Description" });

        config.Locos = JMRIRosterImporter.Import("roster.xml");

        config.Save();

        var loadConfig = Configuration.Configuration.Load(config.Name);
        Assert.That(loadConfig, Is.Not.Null);
        Assert.That(loadConfig, Is.EqualTo(config));
    }

    //[Test]
    //public void ManufacturersTest() {
    //    var mnf = new Manufacturers();
    //    Assert.That(mnf.Count == 169);
    //}

    [Test]
    public void ImportJMRIRoster() {
        var imported = JMRIRosterImporter.Import("systemTests\\roster.xml");
        Assert.That(imported.Count > 0);
    }

    [Test]
    public void DoNothingTest() { }
}