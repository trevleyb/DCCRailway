using System.IO.Ports;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Adapters.Helpers;

public static class SerialAdapterFinder {

    const int MillisecondsDelay = 10;

    /// <summary>
    ///     Static function that will SEARCH for a Port that returns some valid data
    /// </summary>
    /// <param name="data">The query to push to the serial adapter</param>
    /// <returns>The reply from the adapter and the settings used</returns>
    public static async IAsyncEnumerable<(byte[]? result, SerialAdapterSettings settings)> Find(byte[] data) {
        foreach (var port in SerialPort.GetPortNames()) {
            foreach (var baudRate in new[] { 9600, 19200, 38400, 57600, 115200 }) {
                foreach (var parity in new[] { Parity.None, Parity.Even, Parity.Odd, Parity.Mark, Parity.Space }) {
                    foreach (var dataBits in new[] { 7, 8 }) {
                        foreach (var stopBits in new[] { StopBits.None, StopBits.One, StopBits.OnePointFive, StopBits.Two }) {
                            var settings = new SerialAdapterSettings(port, baudRate, dataBits, parity, stopBits, 1000);
                            var result   = await TestSerialPort(settings, data);
                            yield return (result, settings);
                        }
                    }
                }
            }
        }
    }

    public static async Task<byte[]?> TestSerialPort(SerialAdapterSettings settings, byte[] data) {
        var readData = Array.Empty<byte>();

        try {
            using (var connection = new SerialPort(settings.PortName, settings.BaudRate, settings.Parity, settings.DataBits, settings.StopBits)) {
                connection.WriteTimeout = settings.Timeout;
                connection.ReadTimeout  = settings.Timeout;

                connection.Open();
                await connection.BaseStream.WriteAsync(data, 0, data.Length);

                var timeoutTime = DateTime.Now.AddMilliseconds(settings.Timeout);
                while (DateTime.Now <= timeoutTime && connection.BytesToRead == 0) {
                    await Task.Delay(MillisecondsDelay); // Adjust as needed
                }

                if (connection.BytesToRead > 0) {
                    readData = new byte[connection.BytesToRead];
                    _ = await connection.BaseStream.ReadAsync(readData, 0, readData.Length);
                }
            }
        }
        catch (Exception ex) {
            Logger.Log.Debug($"ERROR Connecting to port: {ex.Message}");
        }
        return readData;
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