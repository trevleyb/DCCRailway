using DCCRailway.Common.Types;
using DCCRailway.Railway.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class RailwayManagerTests {

    [Test]
    public void CreateTestFileForTesting() {
        CreateTestFile.Build("DCCRailway.Test.Json");
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