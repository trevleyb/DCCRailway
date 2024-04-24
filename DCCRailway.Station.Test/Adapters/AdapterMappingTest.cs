using NUnit.Framework;
using DCCRailway.Station.Adapters.Base;
using System;
using System.IO.Ports;
using System.Runtime.InteropServices.JavaScript;
using DCCRailway.Station.Adapters.Helpers;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

[TestFixture]
public class AdapterMappingTest {

    // For these tests we use a subclass of Adapter because it is abstract
    private class TestAdapter : Adapter {
        [ParameterMappable] public string AStringValue { get; set; }
        [ParameterMappable] public long ALongValue { get; set; }
        [ParameterMappable] public DateOnly ADateValue { get; set; }
        [ParameterMappable] public byte AByteValue { get; set; }
        [ParameterMappable] public int AIntValue { get; set; }
        [ParameterMappable] public Parity AParityValue { get; set; }
        [ParameterMappable] public StopBits AStopValue { get; set; }
        [ParameterMappable] public ColorEnum AEnumValue { get; set; }
    }

    private class NotMappableAdapter : Adapter {
        public string AStringValue { get; set; }
        public long ALongValue { get; set; }
        public DateOnly ADateValue { get; set; }
        public byte AByteValue { get; set; }
        public int AIntValue { get; set; }
        public Parity AParityValue { get; set; }
        public StopBits AStopValue { get; set; }
        public ColorEnum AEnumValue { get; set; }
    }

    [TestCase]
    public void Test_GetMappablePropertiesOnMappableClass() {
        var adapter = new TestAdapter();
        Assert.That(adapter.GetMappableGetParameters().Count, Is.EqualTo(8));

        adapter.SetMappableParameter("AStringValue", "string");
        adapter.SetMappableParameter("ALongValue", "12345671234567");
        adapter.SetMappableParameter("ADateValue", "2022-01-01");
        adapter.SetMappableParameter("AByteValue", "255");
        adapter.SetMappableParameter("AIntValue", "42");
        adapter.SetMappableParameter("AParityValue", "Even");
        adapter.SetMappableParameter("AStopValue", "Two");
        adapter.SetMappableParameter("AEnumValue", "Green");

        var parameters = adapter.GetMappableGetParameters();

        Assert.That(parameters["AStringValue"], Is.EqualTo("string"));
        Assert.That(parameters["ALongValue"], Is.EqualTo("12345671234567"));
        Assert.That(parameters["AByteValue"], Is.EqualTo("255"));
        Assert.That(parameters["AIntValue"], Is.EqualTo("42"));
        Assert.That(parameters["AParityValue"], Is.EqualTo("Even"));
        Assert.That(parameters["AStopValue"], Is.EqualTo("Two"));
        Assert.That(parameters["AEnumValue"], Is.EqualTo("Green"));
    }


    [TestCase]
    public void Test_GetMappablePropertiesOnNotMappableClass() {
        var adapter = new NotMappableAdapter();
        Assert.That(adapter.GetMappableGetParameters().Count, Is.EqualTo(0));
    }

    [TestCase]
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
    }

}


enum ColorEnum {
    Red,
    Green,
    Yellow,
    Blue
}