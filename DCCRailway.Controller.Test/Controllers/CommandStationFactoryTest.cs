using DCCRailway.Controller.Controllers;

namespace DCCRailway.System.Test.Controllers;

[TestFixture, TestOf(typeof(CommandStationFactory))]
public class CommandStationFactoryTest {
    [Test]
    public void ControllerFactorLoadControllersTest() {
        var factory = new CommandStationFactory();
        Assert.That(factory.Controllers.Count > 0, "Should get a set of controllers from the loader.");
        Assert.That(factory.Controllers.Exists(n => n.Name.Equals("Virtual", StringComparison.InvariantCultureIgnoreCase)), "Should contain the Virtual commandStation.");
    }

    [Test]
    public void ControllerFactorLoadAndInstantiateControllers() {
        var factory = new CommandStationFactory();
        foreach (var controller in factory.Controllers) {
            var instance = controller.Create();
            Assert.That(instance, Is.Not.Null, "Should be able to create an instance of the commandStation.");
            Assert.That(instance, Is.InstanceOf<ICommandStation>(), "Should be an instance of ICommandStation.");
            Assert.That(instance.Adapters?.Count, Is.GreaterThan(0), "Should have at least one adapter registered.");
        }
    }

    [Test]
    public void ControllerFactorLoadAndInstantiateControllersAndCheckCommands() {
        var factory = new CommandStationFactory();
        foreach (var controller in factory.Controllers) {
            var instance = controller.Create();
            Assert.That(instance, Is.Not.Null, "Should be able to create an instance of the commandStation.");
            Assert.That(instance, Is.InstanceOf<ICommandStation>(), "Should be an instance of ICommandStation.");
            Assert.That(instance.Adapters?.Count, Is.GreaterThan(0), "Should have at least one adapter registered.");

            //foreach (var adapter in instance.SupportedAdapters) {
            //    var instanceAdapter = instance.CreateAdapter(adapter.name);
            //    Assert.That(instance.Adapter, Is.Not.Null, "Should be able to create an instance of the adapter.");
            //    instance.Adapter = instanceAdapter;
            //    Assert.That(instance.SupportedCommands.Count, Is.GreaterThan(0), "Should have at least one command registered.");
            //}
        }
    }
}