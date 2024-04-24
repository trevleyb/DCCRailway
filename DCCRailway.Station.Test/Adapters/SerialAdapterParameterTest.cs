using System.IO.Ports;
using DCCRailway.Station.Adapters.Helpers;
using DCCRailway.Station.Helpers;
using DCCRailway.Station.NCE.Adapters;

namespace DCCRailway.System.Test.Adapters;

[TestFixture]
public class SerialAdapterParameterTest {

    [TestCase]
    public void TestThatASerialAdapterParametersCanBeRead() {

        var adapter = new NCESerial();
        adapter.PortName = "\\Dev\\Com1";
        adapter.Timeout = 2000;
        adapter.Parity = Parity.Even;
        adapter.BaudRate = 9600;
        adapter.DataBits = 0;
        adapter.StopBits = StopBits.OnePointFive;

        var parameters = adapter.GetMappableGetParameters();
        Assert.That(parameters["PortName"],Is.EqualTo("\\Dev\\Com1"));
        Assert.That(parameters["Timeout"], Is.EqualTo("2000"));
        Assert.That(parameters["Parity"], Is.EqualTo("Even"));
        Assert.That(parameters["BaudRate"], Is.EqualTo("9600"));
        Assert.That(parameters["DataBits"], Is.EqualTo("0"));
        Assert.That(parameters["StopBits"], Is.EqualTo("OnePointFive"));

    }
    [TestCase]
    public void TestThatASerialAdapterCanBeInjectedWithParameters() {

        var adapter = new NCESerial();
        adapter.SetMappableParameter("PortName","\\Dev\\Com1");
        adapter.SetMappableParameter("Timeout","2000");
        adapter.SetMappableParameter("Parity","Even");
        adapter.SetMappableParameter("BaudRate","9600");
        adapter.SetMappableParameter("DataBits","0");
        adapter.SetMappableParameter("StopBits","OnePointFive");

        Assert.That(adapter.PortName,Is.EqualTo("\\Dev\\Com1"));
        Assert.That(adapter.Timeout, Is.EqualTo(2000));
        Assert.That(adapter.Parity, Is.EqualTo(Parity.Even));
        Assert.That(adapter.BaudRate, Is.EqualTo(9600));
        Assert.That(adapter.DataBits, Is.EqualTo(0));
        Assert.That(adapter.StopBits, Is.EqualTo(StopBits.OnePointFive));

    }

}