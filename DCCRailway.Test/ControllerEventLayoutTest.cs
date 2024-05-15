using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Layout.Entities;
using DCCRailway.Railway;
using DCCRailway.Railway.Configuration.Entities;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class ControllerEventLayoutTest {
    [Test]
    public void TestLayoutCmdProcessorForALoco() {
        var layoutConfig = new RailwayManager();
        Assert.That(layoutConfig, Is.Not.Null);

        layoutConfig.Settings.Controller.Name = "Virtual";
        layoutConfig.Settings.Controller.Adapters.Add(new Adapter { Name = "Virtual" });
        layoutConfig.Start();
        Assert.That(layoutConfig.StateManager, Is.Not.Null);
        Assert.That(layoutConfig.CommandStationManager, Is.Not.Null);

        // Add a Locomotive to the layout configuration
        // ------------------------------------------------
        layoutConfig.Locomotives.Add(new Locomotive { Name = "TestLoco", Address = new DCCAddress(3) });
        var loco = layoutConfig.Locomotives.GetAll().First();
        Assert.That(loco, Is.Not.Null);
        Assert.That(loco!.Name, Is.EqualTo("TestLoco"));

        var setLocoSpeed = layoutConfig.CommandStationManager.CommandStation.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(setLocoSpeed, Is.Not.Null);
        setLocoSpeed!.Address = new DCCAddress(3);
        setLocoSpeed.Speed    = new DCCSpeed(50);
        Assert.That(loco?.Speed?.Value, Is.EqualTo(0));

        var result          = setLocoSpeed.Execute();
        var controllerEvent = new CommandEventArgs(result);
        layoutConfig.StateProcessor.ProcessCommandEvent(controllerEvent);

        //Assert.That(loco?.Speed?.Value, Is.EqualTo(50));
    }

    private ICommandStation CreateVirtualControllerWithAdapter() {
        // Create an instance of a CommandStation using the Factory
        // ------------------------------------------------------------
        var factory       = new CommandStationFactory();
        var virtualSystem = factory.Find("Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        // Check that we can do things with the commandStation
        // ------------------------------------------------------------
        var controller = virtualSystem!.Create();
        Assert.That(controller, Is.Not.Null);

        if (controller is null) throw new NullReferenceException("Should have a CommandStation object at this stage");

        // Now that we have created a CommandStation, we need to create an ADAPTER that we can connect to the
        // ------------------------------------------------------------
        var adapter = controller.CreateAdapter("Virtual");
        Assert.That(adapter, Is.Not.Null);
        controller.Adapter = adapter;

        return controller;
    }
}