using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Controllers;
using DCCRailway.System.Controllers.Events;
using DCCRailway.Layout;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Test.Layout;

using NUnit.Framework;

[TestFixture]
public class ControllerEventLayoutTest {
    [Test]
    public void TestLayoutCmdProcessorForALoco() {
        var layoutConfig       = new DCCRailwayConfig();
        var layoutCmdProcessor = new LayoutCmdUpdater.LayoutCmdUpdater(layoutConfig);
        Assert.That(layoutConfig, Is.Not.Null);
        Assert.That(layoutCmdProcessor, Is.Not.Null);

        // Add a Locomotive to the layout configuration
        // ------------------------------------------------
        layoutConfig.Locomotives.Add(new Locomotive { Name = "TestLoco", Address = new DCCAddress(3) });
        var loco = layoutConfig.Locomotives["TestLoco"];
        Assert.That(loco, Is.Not.Null);

        var controller   = CreateVirtualControllerWithAdapter();
        var setLocoSpeed = controller.CreateCommand<ICmdLocoSetSpeed>();
        Assert.That(setLocoSpeed, Is.Not.Null);
        setLocoSpeed!.Address = new DCCAddress(3);
        setLocoSpeed.Speed    = new DCCSpeed(50);
        Assert.That(loco?.Speed?.Value, Is.EqualTo(0));

        // todo: fix this test
        //var controllerEvent = new CommandEventArgs(setLocoSpeed, CommandResult.Success(), controller.Adapter!);
        //layoutCmdProcessor.ProcessCommandEvent(controllerEvent);
        //Assert.That(loco?.Speed?.Value, Is.EqualTo(50));
    }

    private IController CreateVirtualControllerWithAdapter() {
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