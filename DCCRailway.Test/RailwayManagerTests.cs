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
    public async Task TestInstantiatingTheRailwayManager() {

        var config = await CreateTestConfig();
        var railwayManager = new RailwayManager(config);
        Assert.That(railwayManager, Is.Not.Null);

        // Start Up the Railway Manager
        railwayManager.Startup();
        var locoCmd = railwayManager.ActiveController?.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(locoCmd, Is.Not.Null);

        locoCmd!.Address = new DCCAddress(201);
        locoCmd!.Speed = new DCCSpeed(50);

        railwayManager.ActiveController!.Execute(locoCmd);
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Speed.Value, Is.EqualTo(50));
    }

    [TestCase]
    public async Task TestThatTheTestConfigIsCorrect() {
        var config = await CreateTestConfig();

        Assert.That((await config.Controllers.GetByNameAsync("Virtual"))?.Name, Is.EqualTo("Virtual"));
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Name, Is.EqualTo("Train01"));
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Address?.Address, Is.EqualTo(201));
        Assert.That((await config.Locomotives.GetByNameAsync("Train01"))?.Address?.AddressType, Is.EqualTo(DCCAddressType.Short));

    }

    private async Task<IRailwayConfig> CreateTestConfig() {

        var config = RailwayConfig.New("Test Layout", "Test Layout");

        var controllers = config.Controllers;
        var controller = new Controller {
            Name = "Virtual",
            SendStopOnDisconnect = true,
            Adapter = new Adapter {
                AdapterName = "Virtual"
            }
        };
        await controllers.AddAsync(controller);

        var locomotives = config.Locomotives;
        var locomotive = new Locomotive {
            Name = "Train01",
            Direction = DCCDirection.Forward,
            Speed = new DCCSpeed(0),
            Address = new DCCAddress(201, DCCAddressType.Short, DCCProtocol.DCC28)
        };
        await locomotives.AddAsync(locomotive);

        var sensors = config.Sensors;
        await sensors.AddAsync(new Sensor { Name = "Sensor01", Address = new DCCAddress(501) });
        await sensors.AddAsync(new Sensor { Name = "Sensor02", Address = new DCCAddress(502) });
        await sensors.AddAsync(new Sensor { Name = "Sensor03", Address = new DCCAddress(503) });
        await sensors.AddAsync(new Sensor { Name = "Sensor04", Address = new DCCAddress(504) });
        await sensors.AddAsync(new Sensor { Name = "Sensor05", Address = new DCCAddress(505) });

        var turnouts = config.Turnouts;
        await turnouts.AddAsync(new Turnout { Name = "Turnout01", Address = new DCCAddress(401)} );
        await turnouts.AddAsync(new Turnout { Name = "Turnout02", Address = new DCCAddress(402)} );
        await turnouts.AddAsync(new Turnout { Name = "Turnout03", Address = new DCCAddress(403)} );
        await turnouts.AddAsync(new Turnout { Name = "Turnout04", Address = new DCCAddress(404)} );
        await turnouts.AddAsync(new Turnout { Name = "Turnout05", Address = new DCCAddress(405)} );

        var signals = config.Signals;
        await signals.AddAsync(new Signal { Name = "Signal01", Address = new DCCAddress(301) } );
        await signals.AddAsync(new Signal { Name = "Signal02", Address = new DCCAddress(302) } );
        await signals.AddAsync(new Signal { Name = "Signal03", Address = new DCCAddress(303) } );
        await signals.AddAsync(new Signal { Name = "Signal04", Address = new DCCAddress(304) } );
        await signals.AddAsync(new Signal { Name = "Signal05", Address = new DCCAddress(305) } );

        var blocks = config.Blocks;
        await blocks.AddAsync(new Block { Name = "Block01" });
        await blocks.AddAsync(new Block { Name = "Block02" });
        await blocks.AddAsync(new Block { Name = "Block03" });
        await blocks.AddAsync(new Block { Name = "Block04" });
        await blocks.AddAsync(new Block { Name = "Block05" });

        var accessories = config.Accessories;
        await accessories.AddAsync(new Accessory { Name = "Accy01", Address = new DCCAddress(601) });
        await accessories.AddAsync(new Accessory { Name = "Accy02", Address = new DCCAddress(602) });
        await accessories.AddAsync(new Accessory { Name = "Accy03", Address = new DCCAddress(603) });
        await accessories.AddAsync(new Accessory { Name = "Accy04", Address = new DCCAddress(604) });
        await accessories.AddAsync(new Accessory { Name = "Accy05", Address = new DCCAddress(605) });

        return await Task.FromResult((IRailwayConfig)config);
    }
}