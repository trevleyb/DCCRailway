using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.Managers;
using DCCRailway.WiThrottle.Server;

namespace DCCRailway.TestClasses;

public static class Simulator {
    public static void Run(string[] args) {
        var logger   = LoggerHelper.DebugLogger;
        var settings = new RailwaySettings(logger).CreateSampleData("./", "Sample");
        settings.Controller.Name         = "Virtual";
        settings.Controller.Adapter.Name = "Virtual";

        var stateManager = new StateManager.StateManager();
        var cmdStation   = new ControllerManager(logger, settings.Controller);
        var wii          = new WiThrottle.Server.Server(logger, settings);

        // Turn on the fastClock (normally turned on/off in the UI)
        // --------------------------------------------------------------
        settings.Settings.FastClock.Ratio         = 15;
        settings.Settings.FastClock.State         = FastClockState.Start;
        settings.Settings.WiThrottle.UseFastClock = true;
        logger.Information("Starting the CommandStation (Virtual) Server");
        cmdStation.Start();

        // Start the WiThrottle and run it in the background
        // --------------------------------------------------
        logger.Information("Starting the WiThrottle Server");
        wii.Start(cmdStation.CommandStation!);
        logger.Information("WiThrottle Server should now be running in background.");

        // Wait until we press ENTER to stop the WiThrottle
        // --------------------------------------------------
        logger.Information("Type Q or Quit on Console to finish");
        while (true) {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && (input.ToLower() == "q" || input.ToLower() == "quit")) {
                break;
            }
        }

        logger.Information("Stopping the WiThrottle Server");
        wii.Stop();
        logger.Information("Stopping the Virtual Command Station");
        cmdStation.Stop();
        logger.Information("Finished.");
    }
}