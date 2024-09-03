using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.Managers;
using DCCRailway.WiThrottle.Server;

namespace DCCRailway.TestClasses;

public static class TestWiThrottle {
    public static void WiThrottleRun(string[] args) {
        var logger   = LoggerHelper.DebugLogger;
        var settings = new RailwaySettings(logger).CreateSampleData("./", "Sample");

        // Find the NCE USB Serial Adapter that returns a valid response - send 0x80 and get ! returned
        // --------------------------------------------------------------------------------------------
        //var ports = SerialAdapterFinder.Find(0x80, 0x21, null, [9600], [8], [Parity.None], [StopBits.One]);
        //if (ports.Count == 0) {
        //    logger.Error("No Serial Ports found that match the NCE USB Serial Adapter");
        //    return;
        //}

        // Set the Controller settings for NCEPowerCab for Testing Purposes.
        // ---------------------------------------------------------------------------------------
        settings.Controller.Name         = "Virtual";
        settings.Controller.Adapter.Name = "Virtual";

        // Set the Controller settings for NCEPowerCab for Testing Purposes.
        // ---------------------------------------------------------------------------------------
        //settings.Controller.Name         = "NCEPowerCab";
        //settings.Controller.Adapter.Name = "NCEUSBSerial";
        //settings.Controller.Adapter.Parameters.Add("portName", ports[0].PortName);
        //settings.Controller.Adapter.Parameters.Add("baudRate", ports[0].BaudRate);
        //settings.Controller.Adapter.Parameters.Add("dataBits", ports[0].DataBits);
        //settings.Controller.Adapter.Parameters.Add("stopBits", ports[0].StopBits);
        //settings.Controller.Adapter.Parameters.Add("parity", ports[0].Parity);
        //settings.Controller.Adapter.Parameters.Add("timeout", ports[0].Timeout);

        /*
        settings.Analyser.Name         = "NCE Packet Analyser";
        settings.Analyser.Adapter.Name = "NCESerial";
        settings.Analyser.Adapter.Parameters.Add("portName", "/dev/tty.usbserial-11420");
        settings.Analyser.Adapter.Parameters.Add("baudRate", 38400);
        settings.Analyser.Adapter.Parameters.Add("dataBits", 8);
        settings.Analyser.Adapter.Parameters.Add("stopBits", StopBits.One);
        settings.Analyser.Adapter.Parameters.Add("parity", Parity.None);
        settings.Analyser.Adapter.Parameters.Add("NewLine", "0x0D");
        */

        var stateManager = new StateManager.StateManager();
        var cmdStation   = new ControllerManager(logger, settings.Controller);
        var analyser     = new ControllerManager(logger, settings.Analyser);
        var wii          = new WiThrottle.Server.Server(logger, settings);

        // Turn on the fastClock (normally turned on/off in the UI)
        // --------------------------------------------------------------
        settings.Settings.FastClock.Ratio         = 15;
        settings.Settings.FastClock.State         = FastClockState.Start;
        settings.Settings.WiThrottle.UseFastClock = true;
        cmdStation.Start();
        analyser.Start();

        // Start the WiThrottle and run it in the background
        // --------------------------------------------------
        logger.Information("Starting the WiThrottle Server");
        wii.Start(cmdStation.CommandStation!);
        logger.Information("WiThrottle Server should now be running in background.");

        // Wait until we press ENTER to stop the WiThrottle
        // --------------------------------------------------
        logger.Information("Press ENTER on Console to finish");
        Console.ReadLine();

        logger.Information("Stopping the WiThrottle Server");
        wii.Stop();
        cmdStation.Stop();
        analyser.Stop();
        logger.Information("END");
    }
}