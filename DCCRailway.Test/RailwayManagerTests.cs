using DCCRailway.Application;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Virtual.Commands;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class RailwayManagerTests {

    [TestCase]
    public void TestInstantiatingTheRailwayManager() {

        var config = CreateTestConfig();
        var railwayManager = new RailwayManager(config);
        Assert.That(railwayManager, Is.Not.Null);

        // Start Up the Railway Manager
        railwayManager.Startup();
        var locoCmd = railwayManager.ActiveController?.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(locoCmd, Is.Not.Null);

        locoCmd!.Address = new DCCAddress(201);
        locoCmd!.Speed = new DCCSpeed(50);

        railwayManager.ActiveController!.Execute(locoCmd);
        Assert.That(config.LocomotiveRepository.GetByNameAsync("Train01")?.Result?.Speed.Value, Is.EqualTo(50));
    }

    [TestCase]
    public void TestThatTheTestConfigIsCorrect() {
        var config = CreateTestConfig();
        Assert.That(config.ControllerRepository.GetByNameAsync("Virtual").Result?.Name, Is.EqualTo("Virtual"));
        Assert.That(config.LocomotiveRepository.GetByNameAsync("Train01").Result?.Name, Is.EqualTo("Train01"));
        Assert.That(config.LocomotiveRepository.GetByNameAsync("Train01").Result?.Address?.Address, Is.EqualTo(201));
    }

    private IRailwayConfig CreateTestConfig() {

        var config = RailwayConfig.New("Test Layout", "Test Layout");

        var controllers = config.ControllerRepository;
        var controller = new Controller {
            Name = "Virtual",
            SendStopOnDisconnect = true,
            Adapter = new Adapter {
                AdapterName = "Virtual"
            }
        };
        controllers.AddAsync(controller);

        var locomotives = config.LocomotiveRepository;
        var locomotive = new Locomotive {
            Name = "Train01",
            Direction = DCCDirection.Forward,
            Speed = new DCCSpeed(0),
            Address = new DCCAddress(201, DCCAddressType.Short, DCCProtocol.DCC28)
        };
        locomotives.AddAsync(locomotive);

        var sensors = config.SensorRepository;
        sensors.AddAsync(new Sensor { Name = "Sensor01", Address = new DCCAddress(501) });
        sensors.AddAsync(new Sensor { Name = "Sensor02", Address = new DCCAddress(502) });
        sensors.AddAsync(new Sensor { Name = "Sensor03", Address = new DCCAddress(503) });
        sensors.AddAsync(new Sensor { Name = "Sensor04", Address = new DCCAddress(504) });
        sensors.AddAsync(new Sensor { Name = "Sensor05", Address = new DCCAddress(505) });

        var turnouts = config.TurnoutRepository;
        turnouts.AddAsync(new Turnout { Name = "Turnout01", Address = new DCCAddress(401)} );
        turnouts.AddAsync(new Turnout { Name = "Turnout02", Address = new DCCAddress(402)} );
        turnouts.AddAsync(new Turnout { Name = "Turnout03", Address = new DCCAddress(403)} );
        turnouts.AddAsync(new Turnout { Name = "Turnout04", Address = new DCCAddress(404)} );
        turnouts.AddAsync(new Turnout { Name = "Turnout05", Address = new DCCAddress(405)} );

        var signals = config.SignalRepository;
        signals.AddAsync(new Signal { Name = "Signal01", Address = new DCCAddress(301) } );
        signals.AddAsync(new Signal { Name = "Signal02", Address = new DCCAddress(302) } );
        signals.AddAsync(new Signal { Name = "Signal03", Address = new DCCAddress(303) } );
        signals.AddAsync(new Signal { Name = "Signal04", Address = new DCCAddress(304) } );
        signals.AddAsync(new Signal { Name = "Signal05", Address = new DCCAddress(305) } );

        var blocks = config.BlockRepository;
        blocks.AddAsync(new Block { Name = "Block01" });
        blocks.AddAsync(new Block { Name = "Block02" });
        blocks.AddAsync(new Block { Name = "Block03" });
        blocks.AddAsync(new Block { Name = "Block04" });
        blocks.AddAsync(new Block { Name = "Block05" });

        var accessories = config.AccessoryRepository;
        accessories.AddAsync(new Accessory { Name = "Accy01", Address = new DCCAddress(601) });
        accessories.AddAsync(new Accessory { Name = "Accy02", Address = new DCCAddress(602) });
        accessories.AddAsync(new Accessory { Name = "Accy03", Address = new DCCAddress(603) });
        accessories.AddAsync(new Accessory { Name = "Accy04", Address = new DCCAddress(604) });
        accessories.AddAsync(new Accessory { Name = "Accy05", Address = new DCCAddress(605) });

        return config;
    }
}