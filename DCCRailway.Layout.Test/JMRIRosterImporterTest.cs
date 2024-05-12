using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Converters.JMRI;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class JMRIRosterImporterTest {

    private IRailwayManager mgr;

    [SetUp]
    public void SetUp() {
        mgr = RailwayManager.Load() ?? throw new Exception("Cannot start a Layout Manager");
    }

    [TearDown]
    public void TearDown() {
    }

    [Test]
    public void ImportJMRIRoster() {
        JMRIRosterImporter.Import(mgr!,"roster.xml");
    }

    [Test]
    public void ImportJMRIRosterAndCheck() {
        var sw = new StreamWriter("corruptfile.xml");
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?");
        Assert.That(() => JMRIRosterImporter.Import(mgr!,"corruptfile.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFilename() {
        Assert.That(() => JMRIRosterImporter.Import(mgr!,"invalid.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFileFormat() {
        var sw = new StreamWriter("invalidfile.xml");
        sw.WriteLine("This is not a valid XML file");
        Assert.That(() => JMRIRosterImporter.Import(mgr!,"invalidfile.xml"), Throws.Exception);
    }
}