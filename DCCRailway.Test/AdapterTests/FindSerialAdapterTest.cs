using System.Diagnostics;
using System.IO.Ports;
using DCCRailway.System.Adapters;
using DCCRailway.System.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.AdapterTests;

[TestClass]
public class FindSerialAdapterTest {
    [TestMethod]
    public void TestSerialAdapter() {
        var settings = new SerialAdapterSettings("/dev/cu.SLAB_USBtoUART", 19200, 8, Parity.None, StopBits.One, 1000);
        var result1  = SerialAdapterFinder.TestSerialPort(settings, new byte[] { 0x80 }).Result;
        Assert.IsFalse(result1 == Array.Empty<byte>(), "Should have received a response from the adapter");
        Assert.IsTrue(result1[0] == 33, "Should have received a ! from the Adapter");
        Debug.WriteLine($"Received {result1.ToDisplayValueChars()}");
    }

    [TestMethod]
    public async Task FindSerialAdapter() {
        var found = 0;
        Debug.WriteLine("Scanning for available ports");

        await foreach (var (result, settings) in SerialAdapterFinder.Find(new byte[] { 0x80 })) {
            Debug.WriteLine($"Reply with value '{result?.ToDisplayValueChars()}' on port: {settings?.PortName} @ {settings?.BaudRate},{settings?.DataBits},{settings?.StopBits},{settings?.Parity}");

            if (result is null || result.Length == 0) continue;

            if (result[0] == 33) {
                Debug.WriteLine("Found adapter at {0}", settings?.PortName);
                found++;
            } else {
                Debug.WriteLine("Found {1} at adapter at {0}", settings?.PortName, result.ToString());
            }
        }
        Debug.WriteLine($"Found {found} adapters.");
    }
}