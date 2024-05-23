using System.Diagnostics;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Managers.Controller;
using DCCRailway.Managers.State;

namespace DCCRailway.WiThrottle.Test;

[TestFixture]
public class WiThrottleTest {
    [Test]
    public void TestIfWiThrottleLoadsAndRuns() {
        Trace.Listeners.Add(new ConsoleTraceListener());
        var logger       = LoggerHelper.ConsoleLogger;
        var settings     = new RailwaySettings(logger).Sample("./", "Sample");
        var stateManager = new StateManager();
        var cmdStation   = new ControllerManager(logger, stateManager, settings.Controller);
        var wii          = new Server(logger, settings);
        cmdStation.Start();

        // Start the WiThrottle and run it in the background
        // --------------------------------------------------
        logger.Information("Starting the WiThrottle Service");
        Trace.Flush();

        wii.Start(cmdStation.CommandStation);
        Assert.That(wii, Is.Not.Null);

        // Wait until we press ENTER to stop the WiThrottle
        // --------------------------------------------------
        if (Debugger.IsAttached) {
            logger.Information("Press ENTER on Console to finish");
            Console.ReadLine();
        }

        logger.Information("Stopping the WiThrottle Service");
        wii.Stop();
    }
}