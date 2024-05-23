using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Layout.Converters;

namespace DCCRailway.Test;

[TestFixture]
public class RailwayManagerTests {
    [Test]
    public void CreateTestFileForTesting() {
        var manager =
            new RailwaySettings(LoggerHelper.ConsoleLogger).New($"./Sample{DateTime.Now:yyMMddHHmmss}", "Sample");
        InjectTestData.SampleData(manager);
        manager.Save();
    }

    [Test]
    public void CreateTestFileForTestingAndReload() {
        var manager =
            new RailwaySettings(LoggerHelper.ConsoleLogger).New($"./Sample{DateTime.Now:yyMMddHHmmss}", "Sample");
        var pathname = manager.PathName;
        InjectTestData.SampleData(manager);
        manager.Save();

        var newInstance = new RailwaySettings(LoggerHelper.ConsoleLogger).Load(pathname, "Sample");
        Assert.That(newInstance, Is.Not.Null);
        Assert.That(manager.Accessories.Count, Is.EqualTo(newInstance.Accessories.Count));
    }
}