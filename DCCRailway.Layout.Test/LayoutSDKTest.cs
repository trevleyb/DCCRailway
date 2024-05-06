using DCCRailway.Layout.Importers;
using DCCRailway.Layout.Layout.LayoutSDK;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class LayoutSDKTest {

    private DCCRailway.Layout.LayoutService? _service;

    [SetUp]
    public void SetUp() {

        CreateTestFile.Build("testlayout.json");
        _service = new DCCRailway.Layout.LayoutService();
        _service.Start("testlayout.json");
    }

    [TearDown]
    public void TearDown() {
        if (_service != null) _service.Stop();
        _service = null;
        Console.ReadLine();
    }

    [Test]
    public async Task TestBlocksIsAccessable() {

        var blocks = new Blocks(_service!.ServiceUrl);
        await foreach (var block in blocks.GetAllAsync().ConfigureAwait(false)) {
            Console.WriteLine(block.Id);
        }
        Console.ReadLine();
    }

}