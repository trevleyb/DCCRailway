using DCCRailway.Configuration.Conversion.JMRI;
using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class JMRIRosterImporterTest {

    [Test]
    public void ImportJMRIRoster() {
        var imported = JMRIRosterImporter.Import("roster.xml");
        Assert.That(imported.Count > 0);
    }
}