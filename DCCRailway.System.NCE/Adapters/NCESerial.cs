﻿using System.IO.Ports;
using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Helpers;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE Serial Adapter", AdapterType.Serial)]
public class NCESerial : SerialAdapter, IAdapter {
    public NCESerial(string                portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) : base(portName, baudRate, dataBits, parity, stopBits, timeout) { }
    public NCESerial(SerialAdapterSettings settings) : base(settings) { }
}