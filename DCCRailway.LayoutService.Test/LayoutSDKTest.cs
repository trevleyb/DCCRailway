using DCCRailway.LayoutService.Layout;
using DCCRailway.LayoutService.Layout.LayoutSDK;

namespace DCCRailway.LayoutService.Test;

[TestFixture]
public class LayoutSDKTest {

    private LayoutRepositoryService? service;

    [SetUp]
    public void SetUp() {

        Layout.Importers.CreateTestFile.Build("testlayout.json");
        service = new LayoutRepositoryService("testlayout.json");
        service.Start();
    }

    [TearDown]
    public void TearDown() {
        if (service != null) service.Stop();
        service = null;
        Console.ReadLine();
    }

    [Test]
    public async Task TestBlocksIsAccessable() {

        var blocks = new Blocks(service!.ServiceUrl);
        await foreach (var block in blocks.GetAllAsync().ConfigureAwait(false)) {
            Console.WriteLine(block.Id);
        }
        Console.ReadLine();
    }

}