using DCCRailway.Railway;
using DCCRailway.Railway.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class RailwayManagerTests {
    [Test]
    public void CreateTestFileForTesting() {
        var manager = new RailwayManager($"./MyTestLayout{DateTime.Now:yyMMddHHmmss}", "MyTestLayout");
        InjectTestData.SampleData(manager);
        manager.Save();
    }

    [Test]
    public void CreateTestFileForTestingAndReload() {
        var manager = new RailwayManager($"./MyTestLayout{DateTime.Now:yyMMddHHmmss}", "MyTestLayout");
        var pathname = manager.PathName;
        InjectTestData.SampleData(manager);
        manager.Save();

        var newInstance = new RailwayManager(pathname, "MyTestLayout",true,false);
        Assert.That(newInstance, Is.Not.Null);
        Assert.That(manager.Accessories.Count, Is.EqualTo(newInstance.Accessories.Count));
    }

    [Test]
    public void TestInstantiatingTheRailwayManager() {
        /*
        var testFilename   = "test.json";
        var railwayConfig  = RailwayConfig.Load();
        var railwayManager = LayoutRepositoryManager.Load(testFilename);
        Assert.That(railwayManager, Is.Not.Null);

        // Start Up the Railway Layout
        railwayManager.Start();
        var locoCmd = railwayManager.ActiveController?.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(locoCmd, Is.Not.Null);

        locoCmd!.Address = new DCCAddress(201);
        locoCmd!.Speed = new DCCSpeed(50);

        railwayManager.ActiveController!.Execute(locoCmd);
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Speed.Value, Is.EqualTo(50));
        */
    }
}