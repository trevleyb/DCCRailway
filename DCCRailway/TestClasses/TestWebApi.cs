using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Managers.Controller;
using DCCRailway.Managers.State;

namespace DCCRailway.TestClasses;

public static class TestWebApi {
    public static void WebApiRun(string[] args) {
        var logger       = LoggerHelper.ConsoleLogger;
        var settings     = new RailwaySettings(logger).Sample("./", "Sample");
        var stateManager = new StateManager();
        var cmdStation   = new ControllerManager(logger, stateManager, settings.Controller);
        var webApi       = new WebApi.Server(logger, settings);

        //cmdStation.Start();

        // Start the WiThrottle and run it in the background
        // --------------------------------------------------
        logger.Information("Starting the WebApi Server");
        webApi.Start();
        logger.Information("WebApi Server should now be running in background.");

        // Wait until we press ENTER to stop the WiThrottle
        // --------------------------------------------------
        logger.Information("Press ENTER on Console to finish");
        Console.ReadLine();

        logger.Information("Stopping the WebApi Server");
        webApi.Stop();
        logger.Information("END");
    }
}