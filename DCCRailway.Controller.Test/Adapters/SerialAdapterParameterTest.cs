using System.IO.Ports;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.NCE.Adapters;

namespace DCCRailway.Controller.Test.Adapters;

[TestFixture]
public class SerialAdapterParameterTest {
    [Test]
    public void TestThatASerialAdapterParametersCanBeRead() {
        var adapter = new NCESerial();
        adapter.PortName = "\\Dev\\Com1";
        adapter.Timeout  = 2000;
        adapter.Parity   = Parity.Even;
        adapter.BaudRate = 9600;
        adapter.DataBits = 0;
        adapter.StopBits = StopBits.OnePointFive;

        var parameters = adapter.GetMappableParameters();
        Assert.That(parameters["PortName"].Value, Is.EqualTo("\\Dev\\Com1"));
        Assert.That(parameters["Timeout"].Value, Is.EqualTo("2000"));
        Assert.That(parameters["Parity"].Value, Is.EqualTo("Even"));
        Assert.That(parameters["BaudRate"].Value, Is.EqualTo("9600"));
        Assert.That(parameters["DataBits"].Value, Is.EqualTo("0"));
        Assert.That(parameters["StopBits"].Value, Is.EqualTo("OnePointFive"));
    }

    [Test]
    public void TestThatASerialAdapterCanBeInjectedWithParameters() {
        var adapter = new NCESerial();
        adapter.SetMappableParameter("PortName", "\\Dev\\Com1");
        adapter.SetMappableParameter("Timeout", "2000");
        adapter.SetMappableParameter("Parity", "Even");
        adapter.SetMappableParameter("BaudRate", "9600");
        adapter.SetMappableParameter("DataBits", "0");
        adapter.SetMappableParameter("StopBits", "OnePointFive");

        Assert.That(adapter.PortName, Is.EqualTo("\\Dev\\Com1"));
        Assert.That(adapter.Timeout, Is.EqualTo(2000));
        Assert.That(adapter.Parity, Is.EqualTo(Parity.Even));
        Assert.That(adapter.BaudRate, Is.EqualTo(9600));
        Assert.That(adapter.DataBits, Is.EqualTo(0));
        Assert.That(adapter.StopBits, Is.EqualTo(StopBits.OnePointFive));
    }

    [Test]
    public void LoadParameterInfoAndMatch() {
        var adapter = new NCESerial();
        var info    = adapter.GetMappableParameters();
        Assert.That(info.Count, Is.GreaterThan(0));
    }
}