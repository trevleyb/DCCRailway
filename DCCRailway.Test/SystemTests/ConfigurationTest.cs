using System.IO.Ports;
using DCCRailway.Conversion.JMRI.Roster;
using DCCRailway.System.Config;
using DCCRailway.System.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decoder = DCCRailway.System.Config.Decoder;

namespace DCCRailway.Test;

[TestClass]
public class ConfigurationTest {
    [TestMethod]
    public void SaveConfigFileTest() {
        Configuration config = new() { Name = @"testconfig.xml" };
        config!.Systems.Add(new DCCRailway.System.Config.System("System1"));

        var adapter = new Adapter { Name = "NCEUSB" };
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
        config.Signals.Add(new Signal { Name = "Signal1", Description = "Loco Description", Decoder = decoder, Parameters = parameters });
        config.Sensors.Add(new Sensor { Name = "Sensor1", Description = "Loco Description", Decoder = decoder, Parameters = parameters });
        config.Turnouts.Add(new Turnout { Name = "Turnout1", Description = "Loco Description", Decoder = decoder, Parameters = parameters });
        config.Blocks.Add(new Block { Name = "Block1", Description = "Loco Description" });

        config.Locos = JMRIRosterImporter.Import("roster.xml");

        config.Save();

        var loadConfig = Configuration.Load(config.Name);
        Assert.IsNotNull(loadConfig);
        Assert.AreEqual(loadConfig, config);
    }

    [TestMethod]
    public void ManufacturersTest() {
        var mnf = new Manufacturers();
        Assert.IsTrue(mnf.Count == 169);
    }

    [TestMethod]
    public void ImportJMRIRoster() {
        var imported = JMRIRosterImporter.Import("systemTests\\roster.xml");
        Assert.IsTrue(imported.Count > 0);
    }

    [TestMethod]
    public void DoNothingTest() { }
}