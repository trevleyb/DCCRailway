using System.IO.Ports;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Adapters.Base;

namespace DCCRailway.System.Test.Adapters;

[TestFixture]
public class AdapterMappingTest {

    // For these tests we use a subclass of Adapter because it is abstract
    private class TestAdapter : Adapter {
        public string AStringValue { get; set; }
        public long ALongValue { get; set; }
        public DateOnly ADateValue { get; set; }
        public byte AByteValue { get; set; }
        public int AIntValue { get; set; }
        public Parity AParityValue { get; set; }
        public StopBits AStopValue { get; set; }
        public ColorEnum AEnumValue { get; set; }
        public bool ABoolean { get; set; }
    }

    [Test]
    public void Test_GetMappablePropertiesOnMappableClass() {
        var adapter = new TestAdapter();
        Assert.That(adapter.GetMappableParameters().Count, Is.EqualTo(9));

        adapter.SetMappableParameter("AStringValue", "string");
        adapter.SetMappableParameter("ALongValue", "12345671234567");
        adapter.SetMappableParameter("ADateValue", "2022-01-01");
        adapter.SetMappableParameter("AByteValue", "255");
        adapter.SetMappableParameter("AIntValue", "42");
        adapter.SetMappableParameter("AParityValue", "Even");
        adapter.SetMappableParameter("AStopValue", "Two");
        adapter.SetMappableParameter("AEnumValue", "Green");
        adapter.SetMappableParameter("ABoolean", "True");

        var parameters = adapter.GetMappableParameters();

        Assert.That(parameters["AStringValue"].Value, Is.EqualTo("string"));
        Assert.That(parameters["ALongValue"].Value, Is.EqualTo("12345671234567"));
        Assert.That(parameters["AByteValue"].Value, Is.EqualTo("255"));
        Assert.That(parameters["AIntValue"].Value, Is.EqualTo("42"));
        Assert.That(parameters["AParityValue"].Value, Is.EqualTo("Even"));
        Assert.That(parameters["AStopValue"].Value, Is.EqualTo("Two"));
        Assert.That(parameters["AEnumValue"].Value, Is.EqualTo("Green"));
        Assert.That(parameters["ABoolean"].Value, Is.EqualTo("True"));
    }

    [Test]
    public void Test_MapConfigValueIsCaseInsensitive() {
        var adapter = new TestAdapter();

        adapter.SetMappableParameter("AStringValue", "string");
        Assert.That(adapter.AStringValue, Is.EqualTo("string"));

        adapter.SetMappableParameter("astringValue", "something new");
        Assert.That(adapter.AStringValue, Is.EqualTo("something new"));
    }

    [Test]
    public void Test_MapConfigValue() {
        var adapter = new TestAdapter();

        adapter.SetMappableParameter("AStringValue", "string");
        Assert.That(adapter.AStringValue, Is.EqualTo("string"));

        adapter.SetMappableParameter("ALongValue", "12345671234567");
        Assert.That(adapter.ALongValue, Is.EqualTo(12345671234567));

        adapter.SetMappableParameter("ADateValue", "2022-01-01");
        Assert.That(adapter.ADateValue, Is.EqualTo(new DateOnly(2022, 1, 1)));

        adapter.SetMappableParameter("AByteValue", "255");
        Assert.That(adapter.AByteValue, Is.EqualTo(255));

        adapter.SetMappableParameter("AIntValue", "42");
        Assert.That(adapter.AIntValue, Is.EqualTo(42));

        adapter.SetMappableParameter("AParityValue", "Even");
        Assert.That(adapter.AParityValue, Is.EqualTo(Parity.Even));

        adapter.SetMappableParameter("AStopValue", "Two");
        Assert.That(adapter.AStopValue, Is.EqualTo(StopBits.Two));

        adapter.SetMappableParameter("AEnumValue", "Green");
        Assert.That(adapter.AEnumValue, Is.EqualTo(ColorEnum.Green));

        adapter.SetMappableParameter("ABoolean", "True");
        Assert.That(adapter.ABoolean, Is.EqualTo(true));

        adapter.SetMappableParameter("ABoolean", "False");
        Assert.That(adapter.ABoolean, Is.EqualTo(false));

        adapter.SetMappableParameter("ABoolean", "true");
        Assert.That(adapter.ABoolean, Is.EqualTo(true));

        adapter.SetMappableParameter("ABoolean", "false");
        Assert.That(adapter.ABoolean, Is.EqualTo(false));
    }

}


enum ColorEnum {
    Red,
    Green,
    Yellow,
    Blue
}