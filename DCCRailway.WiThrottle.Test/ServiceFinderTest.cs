using DCCRailway.Common.Helpers;
using DCCRailway.Layout;
using DCCRailway.Managers;
using DCCRailway.WiThrottle.ServiceHelper;

namespace DCCRailway.WiThrottle.Test;

[TestFixture]
public class ServiceFinderTest {
    [Test]
    public void ClientInfoTest() {
        var si = new ServiceInfo(@"DCCRailway\032WiThrottle\032Service._withrottle._tcp.local", "10.0.0.1", 1234);
        Assert.That(si.Name, Is.EqualTo("DCCRailway WiThrottle Service"));
    }

    [Test]
    public void FindServiceTest() {
        var server   = StartServer();
        var services = ServiceFinder.FindServices("withrottle");
        Assert.That(services.Count, Is.GreaterThanOrEqualTo(1));
        Assert.That(services[0].Name, Is.EqualTo("DCCRailway WiThrottle Service"));
        StopServer(server);
    }

    public static Server.Server StartServer() {
        var logger   = LoggerHelper.DebugLogger;
        var settings = new RailwaySettings(logger).CreateSampleData("./", "Sample");
        settings.Controller.Name         = "Virtual";
        settings.Controller.Adapter.Name = "Virtual";
        var stateManager = new StateManager.StateManager();
        var cmdStation   = new ControllerManager(logger, settings.Controller);
        var wii          = new Server.Server(logger, settings);
        cmdStation.Start();
        wii.Start(cmdStation.CommandStation!);
        return wii;
    }

    public static void StopServer(Server.Server server) {
        server.Stop();
    }
}