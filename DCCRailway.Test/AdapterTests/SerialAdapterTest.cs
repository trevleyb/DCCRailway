using System.IO.Ports;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.AdapterTests;

[TestClass]
public class SerialAdapterTest {
    [TestMethod]
    public void OpenAndCloseAdapter() {
        var adapter = new TestSerial("/dev/cu.SLAB_USBtoUART", 19200);
        adapter.Connect();
        adapter.SendData(new byte[] { 0x80 });
        var res = adapter.RecvData();
        adapter.Disconnect();
        Assert.IsNotNull(res, "Should have received a response from the adapter");
    }
}

[Adapter("TestSerial", AdapterType.Serial)]
internal class TestSerial : SerialAdapter {
    internal TestSerial(string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }
}