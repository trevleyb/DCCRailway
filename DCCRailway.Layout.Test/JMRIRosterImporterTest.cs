using DCCRailway.Layout.Conversion.JMRI;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class JMRIRosterImporterTest {
    [Test]
    public void ImportJMRIRoster() {
        JMRIRosterImporter.Import("roster.xml");
    }

    [Test]
    public void ImportJMRIRosterAndCheck() {
        var sw = new StreamWriter("corruptfile.xml");
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?");
        Assert.That(() => JMRIRosterImporter.Import("corruptfile.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFilename() {
        Assert.That(() => JMRIRosterImporter.Import("invalid.xml"), Throws.Exception);
    }

    [Test]
    public void InvalidFileFormat() {
        var sw = new StreamWriter("invalidfile.xml");
        sw.WriteLine("This is not a valid XML file");
        Assert.That(() => JMRIRosterImporter.Import("invalidfile.xml"), Throws.Exception);
    }
}