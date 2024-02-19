using DCCRailway.Layout.Controllers;
using NUnit.Framework;

namespace DCCRailway.Test.System.Controllers;

[TestFixture]
[TestOf(typeof(ControllerFactory))]
public class ControllerFactoryTest {

    [Test]
    public void ControllerFactorLoadControllersTest() {
        var factory = new ControllerFactory();
        Assert.That(factory.Controllers.Count > 0, "Should get a set of controllers from the loader.");
        Assert.That(factory.Controllers.Exists(n => n.Name.Equals("Virtual",StringComparison.InvariantCultureIgnoreCase)), "Should contain the Virtual controller.");
    }

    [Test]
    public void ControllerFactorLoadAndInstantiateControllers() {
        var factory = new ControllerFactory();
        foreach (var controller in factory.Controllers) {
            var instance = controller.Create();
            Assert.That(instance, Is.Not.Null, "Should be able to create an instance of the controller.");
            Assert.That(instance, Is.InstanceOf<IController>(), "Should be an instance of IController.");
            Assert.That(instance.SupportedAdapters.Count, Is.GreaterThan(0), "Should have at least one adapter registered.");
        }
    }

    [Test]
    public void ControllerFactorLoadAndInstantiateControllersAndCheckCommands() {
        var factory = new ControllerFactory();
        foreach (var controller in factory.Controllers) {
            var instance = controller.Create();
            Assert.That(instance, Is.Not.Null, "Should be able to create an instance of the controller.");
            Assert.That(instance, Is.InstanceOf<IController>(), "Should be an instance of IController.");
            Assert.That(instance.SupportedAdapters.Count, Is.GreaterThan(0), "Should have at least one adapter registered.");
            //foreach (var adapter in instance.SupportedAdapters) {
            //    var instanceAdapter = instance.CreateAdapter(adapter.name);
            //    Assert.That(instance.Adapter, Is.Not.Null, "Should be able to create an instance of the adapter.");
            //    instance.Adapter = instanceAdapter;
            //    Assert.That(instance.SupportedCommands.Count, Is.GreaterThan(0), "Should have at least one command registered.");
            //}
        }
    }
}
