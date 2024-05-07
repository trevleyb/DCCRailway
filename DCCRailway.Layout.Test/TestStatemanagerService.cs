using DCCRailway.Common.Configuration;
using DCCRailway.Common.State;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Importers;
using DCCRailway.Layout.StateManager.StateSDK;

namespace DCCRailway.Layout.Test;

public class TestStatemanagerService {

    [Test]
    //[Ignore("Only run manually")]
    public async Task ManualTest() {
        var layoutService = new LayoutService();
        try {
            CreateTestFile.Build("DCCRailway.TestLayout.json");
            var settings = new ServiceSetting("Test", "https://localhost", 5001, "DCCRailway.TestLayout.json");
            Console.WriteLine("Starting");
            await layoutService.Start(settings);

            var states = new States(settings.ServiceURL);
            var ts = new StateObject("T01");
            ts.Add("Turnout", DCCTurnoutState.Thrown);
            states.Save(ts);
            var st = states.Get("T01");
            var allSettings = states.GetAll();

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