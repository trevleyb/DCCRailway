using System.IO.Ports;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Adapters.Helpers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.Managers.Controller;
using DCCRailway.Managers.State;
using DCCRailway.WiThrottle;

namespace DCCRailway.TestClasses;

public static class TestWiThrottle {
    public static void WiThrottleRun(string[] args) {
        var logger   = LoggerHelper.DebugLogger;
        var settings = new RailwaySettings(logger).Sample("./", "Sample");

        // Find the NCE USB Serial Adapter that returns a valid response - send 0x80 and get ! returned
        // --------------------------------------------------------------------------------------------
        var ports = SerialAdapterFinder.Find(0x80, 0x21, null, [9600], [8], [Parity.None], [StopBits.One]);
        if (ports.Count == 0) {
            logger.Error("No Serial Ports found that match the NCE USB Serial Adapter");
            return;
        }

        // Set the Controller settings for NCEPowerCab for Testing Purposes.
        // ---------------------------------------------------------------------------------------
        settings.Controller.Name = "NCEPowerCab";
        settings.Controller.Adapters.Add("NCEUSBSerial");
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("portName", ports[0].PortName);
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("baudRate", ports[0].BaudRate);
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("dataBits", ports[0].DataBits);
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("stopBits", ports[0].StopBits);
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("parity", ports[0].Parity);
        settings.Controller.Adapters["NCEUSBSerial"]?.Parameters.Add("timeout", ports[0].Timeout);
        settings.Controller.DefaultAdapter = "NCEUSBSerial";

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