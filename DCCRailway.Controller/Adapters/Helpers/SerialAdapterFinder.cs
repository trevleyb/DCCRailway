using System.IO.Ports;
using System.Text;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Controller.Adapters.Helpers;

public static class SerialAdapterFinder {
    private const int MillisecondsDelay = 50;

    /// <summary>
    ///     Static function that will SEARCH for a Port that returns some valid data
    /// </summary>
    /// <returns>The reply from the adapter and the settings used</returns>
    public static List<SerialAdapterSettings> Find(byte[] data, byte[] expected, string[]? portName, int[]? baudRate, int[]? dataBits, Parity[]? parity, StopBits[]? stopBits) {
        List<SerialAdapterSettings> foundSettings = [];

        var testBaudRate = baudRate is { Length: > 0 } ? baudRate : new int[] { 9600, 19200, 38400, 57600, 115200 };
        var testParity   = parity is { Length: > 0 } ? parity : new Parity[] { Parity.None, Parity.Even, Parity.Odd, Parity.Mark, Parity.Space };
        var testDataBits = dataBits is { Length: > 0 } ? dataBits : new int[] { 7, 8 };
        var testStopBits = stopBits is { Length: > 0 } ? stopBits : new StopBits[] { StopBits.One, StopBits.OnePointFive, StopBits.Two };

        foreach (var searchPort in SerialPort.GetPortNames())
        foreach (var searchBaudRate in testBaudRate)
        foreach (var searchParity in testParity)
        foreach (var searchDataBits in testDataBits)
        foreach (var searchStopBits in testStopBits) {
            try {
                var testSettings = new SerialAdapterSettings(searchPort, searchBaudRate, searchDataBits, searchParity, searchStopBits, 500);
                var result       = TestSerialPort(testSettings, data);
                if (result.Compare(expected)) foundSettings.Add(testSettings);
            } catch { /* Ignore Errors and continue to next Port */
            }
        }

        return foundSettings;
    }

    public static List<SerialAdapterSettings> Find(byte data, byte expected, string[]? portName = null, int[]? baudRate = null, int[]? dataBits = null, Parity[]? parity = null, StopBits[]? stopBits = null) {
        return Find([data], [expected], portName, baudRate, dataBits, parity, stopBits);
    }

    public static List<SerialAdapterSettings> Find(byte data, string expected, string[]? portName = null, int[]? baudRate = null, int[]? dataBits = null, Parity[]? parity = null, StopBits[]? stopBits = null) {
        return Find([data], Encoding.ASCII.GetBytes(expected), portName, baudRate, dataBits, parity, stopBits);
    }

    public static List<SerialAdapterSettings> Find(string data, string expected, string[]? portName = null, int[]? baudRate = null, int[]? dataBits = null, Parity[]? parity = null, StopBits[]? stopBits = null) {
        return Find(Encoding.ASCII.GetBytes(data), Encoding.ASCII.GetBytes(expected), portName, baudRate, dataBits, parity, stopBits);
    }

    public static List<SerialAdapterSettings> Find(string data, byte[] expected, string[]? portName = null, int[]? baudRate = null, int[]? dataBits = null, Parity[]? parity = null, StopBits[]? stopBits = null) {
        return Find(Encoding.ASCII.GetBytes(data), expected, portName, baudRate, dataBits, parity, stopBits);
    }

    public static List<SerialAdapterSettings> Find(byte[] data, string expected, string[]? portName = null, int[]? baudRate = null, int[]? dataBits = null, Parity[]? parity = null, StopBits[]? stopBits = null) {
        return Find(data, Encoding.ASCII.GetBytes(expected), portName, baudRate, dataBits, parity, stopBits);
    }

    public static byte[]? TestSerialPort(SerialAdapterSettings settings, byte[] data) {
        using var connection = new SerialPort(settings.PortName, settings.BaudRate, settings.Parity, settings.DataBits, settings.StopBits);
        connection.WriteTimeout = settings.Timeout;
        connection.ReadTimeout  = settings.Timeout;

        try {
            connection.Open();
            connection.Write(data, 0, data.Length);
            Thread.Sleep(MillisecondsDelay); // Give the connection time to respond.
            var readDataBytes = new byte[connection.BytesToRead];
            connection.Read(readDataBytes, 0, connection.BytesToRead); // Read the data (if any
            Encoding.ASCII.GetString(readDataBytes);
            connection.Close();
            return readDataBytes;
        } catch (Exception ex) {
            throw new Exception($"ERROR Connecting to port: {ex.Message}");
        } finally {
            connection.Close();
        }
    }

    /* Old NON-AWAITable version

    public static async Task<byte[]?> TestSerialPort(SerialAdapterSettings settings, byte[] data) {
        var readBytes = 0;
        var readData  = Array.Empty<byte>();

        try {
            var connection = new SerialPort(settings.PortName, settings.BaudRate, settings.Parity, settings.DataBits, settings.StopBits) { WriteTimeout = settings.Timeout, ReadTimeout = settings.Timeout };

            connection.Open();
            connection.Write(data, 0, data.Length);

            var timeoutTime = DateTime.Now.AddMilliseconds(settings.Timeout);
            while (DateTime.Now <= timeoutTime && (readBytes == 0 || connection.BytesToRead > 0)) {
                if (connection.BytesToRead > 0) {
                    readData  = new byte[connection.BytesToRead];
                    readBytes = connection.Read(readData, 0, connection.BytesToRead);
                }
            }

            // Stop here if readBytes is false meaning we did not read anything and it timed out
            if (readBytes == 0) {
                readData = Array.Empty<byte>();
                Console.WriteLine("stop here");
            }
            connection.Close();
        } catch (Exception ex) {
            readData = Array.Empty<byte>();
            Logger.Log.Debug($"ERROR Connecting to port: {ex.Message}");
        }
        return readData;
    }
    */
}