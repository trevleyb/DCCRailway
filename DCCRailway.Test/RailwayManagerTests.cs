using DCCRailway.Railway;
using DCCRailway.Railway.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class RailwayManagerTests {
    [Test]
    public void CreateTestFileForTesting() {
        var manager = RailwayManager.New("MyTestLayout");
        manager.PathName = $"./MyTestLayout{DateTime.Now:yyMMddHHmmss}";
        InjectTestData.SampleData(manager);
        manager.Save();
    }

    [Test]
    public void CreateTestFileForTestingAndReload() {
        var manager  = RailwayManager.New("MyTestLayout");
        var pathname = $"./MyTestLayout{DateTime.Now:yyMMddHHmmss}";
        manager.PathName = pathname;
        InjectTestData.SampleData(manager);
        manager.Save();

        var newInstance = RailwayManager.Load("MyTestLayout", pathname);
        Assert.That(newInstance, Is.Not.Null);
        Assert.That(manager.Accessories.Count, Is.EqualTo(newInstance.Accessories.Count));
    }

    [Test]
    public async Task TestInstantiatingTheRailwayManager() {
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

    [Test]
    public async Task TestThatTheTestConfigIsCorrect() {
        /*
        var config = await CreateTestConfig();
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Name, Is.EqualTo("Train01"));
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Address?.Address, Is.EqualTo(201));
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Address?.AddressType, Is.EqualTo(DCCAddressType.Short));
        */
    }
}