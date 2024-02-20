using System.Diagnostics;
using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Controllers;
using DCCRailway.Layout.Controllers.Events;
using DCCRailway.Utilities;
using NUnit.Framework;

namespace DCCRailway.Test.Layout;

[TestFixture]
public class VirtualTest {

    [Test]
    public void TestControllerCreation() {
        var controller = new ControllerFactory().CreateController("Virtual");
        Assert.That(controller, Is.Not.Null);
        Assert.That(controller.AttributeInfo().Name.Equals("Virtual"));
    }
    
    [Test]
    public void ComplexAndCompleteVirtualTest() {
        
        // Create an instance of a Controller using the Factory 
        // ------------------------------------------------------------
        var factory = new ControllerFactory();
        var virtualSystem = factory.Find("Virtual");
        Assert.That(virtualSystem, Is.Not.Null);

        // Should have a ControllerInfo object at this stage
        // ------------------------------------------------------------
        if (virtualSystem is null) throw new NullReferenceException("Should have a ControllerInfo object at this stage");
        Assert.That(virtualSystem!.Name.Equals("Virtual"));
        
        // Check that we can do things with the controller
        // ------------------------------------------------------------
        var controller = virtualSystem.Create();
        Assert.That(controller, Is.Not.Null);
        if (controller is null) throw new NullReferenceException("Should have a Controller object at this stage");

        controller.ControllerEvent += ControllerEventArgsHandler;
        
        Assert.That(controller.AttributeInfo().Name.Equals("Virtual"));
        var supportedAdapters = controller.SupportedAdapters;
        Assert.That(supportedAdapters!.Count == 1);
        var supportedCommands = controller?.SupportedCommands;
        Assert.That(supportedCommands!.Count == 0);  // There are no available commands till we attach an adapter
        
        // Now that we have created a Controller, we need to create an ADAPTER that we can connect to the 
        // controller so that the controller can talk to the hardware.
        // ------------------------------------------------------------
        var virtualAdapter = controller?.CreateAdapter("Virtual");
        Assert.That(virtualAdapter, Is.Not.Null);
        if (virtualAdapter is null) throw new NullReferenceException("Should have a VirtualAdapter object at this stage");
        
        // Attach the Adapter to the Controller
        // ------------------------------------------------------------
        controller!.Adapter = virtualAdapter;
        Assert.That(controller.Adapter, Is.Not.Null);        
        Assert.That(controller?.SupportedCommands!.Count > 0);  // After attaching Adapter, should have commands

        // Connect ot the events of both the Controller and the Adapter
        // ------------------------------------------------------------
        
        
        //controller.SystemEvent += (sender, args) => Debug.WriteLine($"Controller Event: {args.Message}");
        //virtualAdapter.SystemEvent += (sender, args) => Debug.WriteLine($"Adapter Event: {args.Message}");
        
        
        // At this stage, we should be able to execute commands against the Controller. 
        // ------------------------------------------------------------
        
        Debug.WriteLine("Stop Here");

    }

    private static void ControllerEventArgsHandler(object? sender, ControllerEventArgs e) {
        Debug.WriteLine(e.Message);
    }
}