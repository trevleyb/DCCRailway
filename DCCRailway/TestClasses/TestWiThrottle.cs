using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.Managers.Controller;
using DCCRailway.Managers.State;
using DCCRailway.WiThrottle;

namespace DCCRailway.TestClasses;

public static class TestWiThrottle {
    public static void WiThrottleRun(string[] args) {
        var logger       = LoggerHelper.ConsoleLogger;
        var settings     = new RailwaySettings(logger).Sample("./", "Sample");
        var stateManager = new StateManager();
        var cmdStation   = new ControllerManager(logger, stateManager, settings.Controller);
        var wii          = new Server(logger, settings);

        // Turn on the fastClock (normally turned on/off in the UI)
        // --------------------------------------------------------------
        settings.Settings.FastClock.Ratio         = 15;
        settings.Settings.FastClock.State         = FastClockState.Start;
        settings.Settings.WiThrottle.UseFastClock = true;
        cmdStation.Start();

        // Start the WiThrottle and run it in the background
        // --------------------------------------------------
        logger.Information("Starting the WiThrottle Server");
        wii.Start(cmdStation.CommandStation);
        logger.Information("WiThrottle Server should now be running in background.");

        // Wait until we press ENTER to stop the WiThrottle
        // --------------------------------------------------
        logger.Information("Press ENTER on Console to finish");
        Console.ReadLine();

        logger.Information("Stopping the WiThrottle Server");
        wii.Stop();
        logger.Information("END");
    }
}