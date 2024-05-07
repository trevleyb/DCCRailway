using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.Common.Types;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.CmdStation.Controllers.Events;
using DCCRailway.Layout.Layout.Entities;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class ControllerEventLayoutTest {

    [Test]
    public void TestLayoutCmdProcessorForALoco() {
        var layoutConfig       = RailwayConfig.New();
        var layoutCmdProcessor = new LayoutUpdater();
        Assert.That(layoutConfig, Is.Not.Null);
        Assert.That(layoutCmdProcessor, Is.Not.Null);

        // Add a Locomotive to the layout configuration
        // ------------------------------------------------
        layoutConfig.Locomotives.Add(new Locomotive { Name = "TestLoco", Address = new DCCAddress(3) });
        var loco = layoutConfig.Locomotives.GetAll().First();
        Assert.That(loco, Is.Not.Null);
        Assert.That(loco!.Name, Is.EqualTo("TestLoco"));

        var controller   = CreateVirtualControllerWithAdapter();
        var setLocoSpeed = controller.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(setLocoSpeed, Is.Not.Null);
        setLocoSpeed!.Address = new DCCAddress(3);
        setLocoSpeed.Speed    = new DCCSpeed(50);
        Assert.That(loco?.Speed?.Value, Is.EqualTo(0));

        // TODO: fix this test
        var controllerEvent = new CommandEventArgs(setLocoSpeed, CommandResult.Success());
        layoutCmdProcessor.ProcessCommandEvent(controllerEvent);
        //Assert.That(loco?.Speed?.Value, Is.EqualTo(50));

    }

     IController CreateVirtualControllerWithAdapter() {
        // Create an instance of a Controller using the Factory 
        // ------------------------------------------------------------
        var factory       = new ControllerFactory();
        var virtualSystem = factory.Find("Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        // Check that we can do things with the controller
        // ------------------------------------------------------------
        var controller = virtualSystem!.Create();
        Assert.That(controller, Is.Not.Null);

        if (controller is null) throw new NullReferenceException("Should have a Controller object at this stage");

        // Now that we have created a Controller, we need to create an ADAPTER that we can connect to the
        // ------------------------------------------------------------
        var adapter = controller.CreateAdapter("Virtual");
        Assert.That(adapter, Is.Not.Null);
        controller.Adapter = adapter;

        return controller;
    }
}