using DCCRailway.Common.Configuration;
using DCCRailway.Layout.Importers;

namespace DCCRailway.Layout.Test;

public class ManualLayoutManager {

    [Test]
    [Ignore("Only run manually")]
    public async Task ManualTest() {
        var layoutService = new LayoutService();
        try {
            CreateTestFile.Build("DCCRailway.TestLayout.json");
            var settings = new ServiceSetting("Test", "https://localhost", 5001, "DCCRailway.TestLayout.json");
            Console.WriteLine("Starting");
            await layoutService.Start(settings);
            Console.ReadLine();
            Console.WriteLine("Finished");
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        finally {
            layoutService.Stop();
        }
    }

}