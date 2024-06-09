using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Virtual.Adapters;
using DCCRailway.StateManager;
using DCCRailway.StateManager.Updater.CommandUpdater;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class ControllerEventLayoutTest {
    [Test]
    public void TestSimpleEventToState() {
        // Create a Virtual Controller so we can issue commands and test results.
        // ----------------------------------------------------------------------------
        var stateManager = new StateManager.StateManager();
        var controller   = new CommandStationFactory(LoggerHelper.DebugLogger).Find("Virtual")?.Create(new VirtualConsoleAdapter(LoggerHelper.DebugLogger));
        Assert.That(controller, Is.Not.Null);

        // At this point, we have a virtual Controller that we can issue commands against, it is wired to the event
        // processor which processes ICmdResult messages and updates a State Manager to track the state of objects
        // within the railway layout. An object needs to have, for the most part, an ADDRESS that we are tracking
        // as commands are only issued to ADDRESSES
        // ------------------------------------------------------------------------------------------------------------
        if (controller is not null) {
            controller.ControllerEvent += (sender, args) => { CmdStateUpdater.Process(args, stateManager); };

            // Try setting a Loco Speed
            // ----------------------------------------------------------------------------------------------------
            var cmdSetSpeed = controller.CreateCommand<ICmdLocoSetSpeed>();

            if (cmdSetSpeed is not null) {
                cmdSetSpeed.Address = new DCCAddress(1024);
                cmdSetSpeed.Speed   = new DCCSpeed(50);
                var setSpeedOld = stateManager.GetState<DCCSpeed>(cmdSetSpeed.Address, StateType.Speed, new DCCSpeed(0));
                Assert.That(setSpeedOld, Is.EqualTo(new DCCSpeed(0)));
                cmdSetSpeed.Execute();
                var setSpeedNew = stateManager.GetState<DCCSpeed>(cmdSetSpeed.Address, StateType.Speed);
                Assert.That(setSpeedNew, Is.EqualTo(new DCCSpeed(50)));
            }

            // Try setting a Loco Momentum
            // ----------------------------------------------------------------------------------------------------
            var cmdSetMomentum = controller.CreateCommand<ICmdLocoSetMomentum>();

            if (cmdSetMomentum is not null) {
                cmdSetMomentum.Address  = new DCCAddress(1024);
                cmdSetMomentum.Momentum = new DCCMomentum(5);
                var cmdSetMomentumOld = stateManager.GetState<DCCMomentum>(cmdSetMomentum.Address, StateType.Momentum, new DCCMomentum(0));
                Assert.That(cmdSetMomentumOld, Is.EqualTo(new DCCMomentum(0)));
                cmdSetMomentum.Execute();
                var cmdSetMomentumNew = stateManager.GetState<DCCMomentum>(cmdSetMomentum.Address, StateType.Momentum);
                Assert.That(cmdSetMomentumNew, Is.EqualTo(new DCCMomentum(5)));
            }
        }
    }

    private ICommandStation CreateVirtualControllerWithAdapter() {
        // Create an instance of a CommandStation using the Factory
        // ------------------------------------------------------------
        var factory       = new CommandStationFactory(LoggerHelper.DebugLogger);
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