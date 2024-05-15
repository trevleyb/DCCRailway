using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;

namespace DCCRailway.Controller.Test;

[TestFixture]
public class VirtualTest {
    private bool _controllerEventFired;

    [Test]
    public void TestControllerCreation() {
        var controller = new CommandStationFactory().CreateController("Virtual");
        Assert.That(controller, Is.Not.Null);
        Assert.That(controller.AttributeInfo().Name.Equals("Virtual"));
    }

    [Test]
    public void TestControllerCreationAndGetCommands() {
        var controller = new CommandStationFactory().CreateController("Virtual");
        Assert.That(controller, Is.Not.Null);
        Assert.That(controller.AttributeInfo().Name.Equals("Virtual"));
        var supportedCommands = controller.Commands;
        Assert.That(supportedCommands!.Count >= 1);
        var command = controller.CreateCommand<IDummyCmd>();
        Assert.That(command, Is.Not.Null);
    }

    [Test]
    public void CreateVirtualControllerAndAddVirtualAdapterTest() {
        var res = CreateVirtualControllerAndAddVirtualAdapter();
    }

    private ICommandStation CreateVirtualControllerAndAddVirtualAdapter() {
        // Create an instance of a CommandStation using the Factory 
        // ------------------------------------------------------------
        var factory       = new CommandStationFactory();
        var virtualSystem = factory.Find("Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        // Should have a ControllerManager object at this stage
        // ------------------------------------------------------------
        if (virtualSystem is null) throw new NullReferenceException("Should have a ControllerManager object at this stage");
        Assert.That(virtualSystem!.Name.Equals("Virtual"));

        // Check that we can do things with the commandStation
        // ------------------------------------------------------------
        var controller = virtualSystem.Create();

        if (controller is null) throw new NullReferenceException("Should have a CommandStation object at this stage");

        _controllerEventFired      =  false;
        controller.ControllerEvent += ControllerEventArgsHandler;

        Assert.That(controller.AttributeInfo().Name.Equals("Virtual"));
        var supportedAdapters = controller.Adapters;
        Assert.That(supportedAdapters!.Count >= 1);
        var supportedCommands = controller?.Commands;
        Assert.That(supportedCommands!.Count >= 1); // There are no available commands till we attach an adapter

        // Now that we have created a CommandStation, we need to create an ADAPTER that we can connect to the 
        // commandStation so that the commandStation can talk to the hardware.
        // ------------------------------------------------------------
        var virtualAdapter = controller!.CreateAdapter("Virtual");
        Assert.That(virtualAdapter, Is.Not.Null);

        if (virtualAdapter is null) throw new NullReferenceException("Should have a VirtualAdapter object at this stage");

        // Attach the Adapter to the CommandStation
        // ------------------------------------------------------------
        controller.Adapter = virtualAdapter;
        Assert.That(controller.Adapter, Is.Not.Null);
        Assert.That(controller.Commands!.Count > 0); // After attaching Adapter, should have commands

        Assert.That(_controllerEventFired, Is.True, "ControllerEvent should have been fired");
        controller.ControllerEvent -= ControllerEventArgsHandler;

        // Should not ever get an error here or something else has seriously gone wrong
        return controller! ?? throw new InvalidOperationException();

        void ControllerEventArgsHandler(object? sender, ControllerEventArgs e) {
            _controllerEventFired = true;
        }
    }

    /*
    [Test]
    public void TestVirtualControllerCommands() {
        var commandsExecuted = new Dictionary<Type, bool>();

        var commandStation = CreateVirtualControllerAndAddVirtualAdapter();
        commandStation.ControllerEvent += ControllerOnControllerEvent; //+= ControllerOnControllerEvent;

        ExecuteCommandAndMonitor<IDummyCmd>(commandStation);

        foreach (var command in commandsExecuted) {
            Assert.That(command.Value, Is.True);
        }

        return;

        // Execute the command and add it to a list that we have executed it, but that its event is false
        // -------------------------------------------------------------------------------------------
        void ExecuteCommandAndMonitor<T>(ICommandStation controllerToCall) where T : ICommand {
            var execCommand = controllerToCall.CreateCommand<T>();
            Assert.That(execCommand, Is.Not.Null);
            if (execCommand != null) {
                commandsExecuted.Add(typeof(T), false);
                controllerToCall.Execute(execCommand);
            }
        }

        // Manage the events coming back from the commandStation and indicate that we received the event
        void ControllerOnControllerEvent(object? sender, IControllerEventArgs e) {
            switch (e) {
            case CommandEventArgs exec:
                switch (exec.Command) {
                case IDummyCmd:
                    commandsExecuted[typeof(IDummyCmd)] = true;

                    break;
                default:
                    throw new Exception("Unexpected type of command executed.");
                }

                break;
            case ControllerEventAdapterAdd:
                // For testing we don't care about this one
                break;
            case ControllerEventAdapterDel:
                // For testing we don't care about this one
                break;
            case AdapterEventArgs:
                // For testing we don't care about this one
                break;
            default:
                throw new Exception("Unexpected type of event raised.");
            }
        }
    }

    private void ControllerOnControllerEvent(object? sender, ControllerEventArgs e) {
        throw new NotImplementedException();
    }
    */
}