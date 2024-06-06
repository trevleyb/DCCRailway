using System.Collections.Generic;
using System.IO.Ports;
using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Adapters.Helpers;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Exceptions;
using Serilog;

namespace DCCRailway.Controller.NCE.Adapters;

[Adapter("NCE Serial Adapter", AdapterType.Serial)]
public class NCESerial(ILogger logger) : SerialAdapter(logger), IAdapter {
    // Helper functions to find a Serial port that the system is connected to.
    // -----------------------------------------------------------------------------------------
    private List<SerialAdapterSettings>? _validPorts;

    public NCESerial(ILogger logger, string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : this(logger) {
        PortName = portName;
        BaudRate = baudRate;
        DataBits = dataBits;
        Parity   = parity;
        StopBits = stopBits;
        Timeout  = timeout;
    }

    public override        List<SerialAdapterSettings> ValidPorts   => _validPorts ??= SerialAdapterFinder.Find(0x80, "!", null, [9600, 19200], [7, 8], [Parity.None], [StopBits.One]);
    public sealed override SerialAdapterSettings?      ValidSetting => (ValidPorts.Count > 0) ? ValidPorts[0] : null;

    public override void ConfigureValidSettings() {
        if (ValidSetting is not null) {
            PortName = ValidSetting.PortName;
            BaudRate = ValidSetting.BaudRate;
            DataBits = ValidSetting.DataBits;
            Parity   = ValidSetting.Parity;
            StopBits = ValidSetting.StopBits;
            Timeout  = ValidSetting.Timeout;
        }

        throw new AdapterException(this, "No valid Serial port found");
    }
}