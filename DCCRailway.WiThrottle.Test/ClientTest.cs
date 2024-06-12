using System.Diagnostics;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Managers;
using DCCRailway.WiThrottle.ServiceHelper;

namespace DCCRailway.WiThrottle.Test;

[TestFixture]
public class ClientTest {
    private bool _connected = false;

    [Test]
    public void RunClientTest() {
        var server = StartServer();
        if (server is null) {
            Assert.Fail("Server failed to start");
            return;
        }

        // Server needs a second to startup.
        Thread.Sleep(100);
        var services = ServiceFinder.FindServices("withrottle");
        var client   = new Client.Client(services[0].ClientInfo);
        client.DataReceived    += ClientOnDataReceived;
        client.ConnectionError += ClientOnConnectionError;
        client.Connect();
        var count = 0;

        // Sleep for 30 secods to allow a couple of heartbeats to get sent before we exit
        while (count++ < 300) {
            Thread.Sleep(100);
        }

        client.Stop();
        server.Stop();
        Assert.That(_connected, Is.True);
    }

    private void ClientOnConnectionError(string obj) {
        Debug.WriteLine(obj);
    }

    private void ClientOnDataReceived(string obj) {
        _connected = true;
        Debug.WriteLine(obj);
    }

    public static Server.Server? StartServer() {
        var logger   = LoggerHelper.DebugLogger;
        var settings = new RailwaySettings(logger).CreateSampleData("./", "Sample");
        settings.Controller.Name                  = "Virtual";
        settings.Controller.Adapter.Name          = "Virtual";
        settings.WiThrottlePrefs.HeartbeatSeconds = 1;
        var stateManager = new StateManager.StateManager();
        var cmdStation   = new ControllerManager(logger, settings.Controller);
        var wii          = new Server.Server(logger, settings);
        cmdStation.Start();
        var result = wii.Start(cmdStation.CommandStation!);
        Console.WriteLine(result.Message);
        return result.Failed ? null : wii;
    }

    public static void StopServer(Server.Server server) {
        server.Stop();
    }
}