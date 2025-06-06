using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Test.Attributes;

[TestFixture]
public class CommandAttributeTests {
    [Test]
    public void TestThatAnythingIsSupported() {
        // The idea is that a COmmand is available IF the selected Adapter matches and the version matches
        // what has been set in the attribute. So we can say that a command is only available for a specific
        // adapter or is available for all BUT a specific adapter (or version).
        // We can add a ! at the front to exclude.
        // We can use * to say ALL or leave it blank.

        // This command supports ANYTHING
        var attr = new CommandAttribute("TestCommand", "Test Description", "1.0");
        Assert.That(attr.IsSupported("*"), Is.True);
        Assert.That(attr.IsSupported("*@*"), Is.True);
        Assert.That(attr.IsSupported("*", "*"), Is.True);
        Assert.That(attr.IsSupported("Adapter1"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1", "1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1", "2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@*"), Is.True);
        Assert.That(attr.IsSupported("Adapter1", "*"), Is.True);
        Assert.That(attr.IsSupported("Adapter2"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@*"), Is.True);
    }

    [Test]
    public void TestThatOnlyAdapter1IsSupported() {
        // This command is ONLY supported by Adapter1 but should work for ALL versions
        var attr = new CommandAttribute("TestCommand", "Test Description", "1.0", new[] { "Adapter1" });
        Assert.That(attr.IsSupported("Adapter1"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter1@*"), Is.True);
        Assert.That(attr.IsSupported("Adapter2"), Is.False);
        Assert.That(attr.IsSupported("Adapter2@1.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter2@2.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter2@*"), Is.False);
    }

    [Test]
    public void TestThatAnythingExceptAdapter1isSupported() {
        // This command will work with anything EXCEPT Adapter1
        var attr = new CommandAttribute("TestCommand", "Test Description", "1.0", null, new[] { "Adapter1" });
        Assert.That(attr.IsSupported("Adapter2"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@*"), Is.True);
        Assert.That(attr.IsSupported("Adapter1"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@1.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@2.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@*"), Is.False);
    }

    [Test]
    public void TestThatAnytingExceptAdapter1or3IsSupported() {
        // This command will work with anything EXCEPT Adapter1 or adapter 3
        var attr = new CommandAttribute("TestCommand", "Test Description", "1.0", null, new[] { "Adapter1", "Adapter3@*" });
        Assert.That(attr.IsSupported("Adapter2"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@1.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@2.0"), Is.True);
        Assert.That(attr.IsSupported("Adapter2@*"), Is.True);
        Assert.That(attr.IsSupported("Adapter1"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@1.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@2.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter1@*"), Is.False);
        Assert.That(attr.IsSupported("Adapter3"), Is.False);
        Assert.That(attr.IsSupported("Adapter3@1.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter3@2.0"), Is.False);
        Assert.That(attr.IsSupported("Adapter3@*"), Is.False);
    }

    [Test]
    public void IsSupported_ReturnsTrue_WhenAdapterAndVersionAreIncluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", new[] { "Adapter1@1.0" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.True);
    }

    [Test]
    public void IsSupported_ReturnsFalse_WhenAdapterIsExcluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", null, new[] { "Adapter1@1.0" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.False);
    }

    [Test]
    public void IsSupported_ReturnsTrue_WhenAllAdaptersAndVersionsAreIncluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", new[] { "*@*" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.True);
    }

    [Test]
    public void IsSupported_ReturnsFalse_WhenAllAdaptersAndVersionsAreExcluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", null, new[] { "*@*" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.False);
    }

    [Test]
    public void IsSupported_ReturnsTrue_WhenOnlyAdapterIsIncluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", new[] { "Adapter1" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.True);
    }

    [Test]
    public void IsSupported_ReturnsTrue_WhenOnlyVersionIsIncluded() {
        var commandAttribute = new CommandAttribute("TestCommand", "Test Description", "1.0", new[] { "@1.0" });
        Assert.That(commandAttribute.IsSupported("Adapter1@1.0"), Is.True);
    }
}