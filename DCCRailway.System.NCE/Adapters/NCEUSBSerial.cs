﻿using System.IO.Ports;
using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Helpers;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE USB Serial Adapter", AdapterType.USB)]
public class NCEUSBSerial : SerialAdapter, IAdapter {
    public NCEUSBSerial(string                portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }
    public NCEUSBSerial(SerialAdapterSettings settings) : base(settings) { }
}