using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Converters.JMRI;
using Serilog;
using Serilog.Core;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class JMRIRosterImporterTest {
    [SetUp]
    public void SetUp() {
        mgr = new RailwaySettings(LoggerHelper.ConsoleLogger) ?? throw new Exception("Cannot start a Layout Manager");
    }

    [TearDown]
    public void TearDown() { }

    private IRailwaySettings mgr;

    [Test]
    public void ImportJMRIRoster() {
        var importer = new JmriRosterImporter(new LoggerConfiguration().CreateLogger());
        importer.Import(mgr.Locomotives, "roster.xml");
    }

    [Test]
    public void ImportJMRIRosterAndCheck() {
        var sw = new StreamWriter("corruptfile.xml");
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?");
        var importer = new JmriRosterImporter(new LoggerConfiguration().CreateLogger());
        Assert.That(() => importer.Import(mgr.Locomotives, "corruptfile.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFilename() {
        var importer = new JmriRosterImporter(new LoggerConfiguration().CreateLogger());
        Assert.That(() => importer.Import(mgr.Locomotives, "invalid.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFileFormat() {
        var sw = new StreamWriter("invalidfile.xml");
        sw.WriteLine("This is not a valid XML file");
        var importer = new JmriRosterImporter(new LoggerConfiguration().CreateLogger());
        Assert.That(() => importer.Import(mgr.Locomotives, "invalidfile.xml"), Throws.Exception);
    }
}