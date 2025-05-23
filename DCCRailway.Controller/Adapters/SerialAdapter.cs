﻿using System.IO.Ports;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Adapters.Events;
using DCCRailway.Controller.Adapters.Helpers;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Exceptions;
using Serilog;

namespace DCCRailway.Controller.Adapters;

public abstract class SerialAdapter(ILogger logger) : Adapter, IAdapter {
    private SerialPort? _connection;

    protected SerialAdapter(ILogger logger, SerialAdapterSettings settings) : this(logger) {
        PortName = settings.PortName;
        BaudRate = settings.BaudRate;
        DataBits = settings.DataBits;
        Parity   = settings.Parity;
        StopBits = settings.StopBits;
        Timeout  = settings.Timeout;
    }

    [Parameter("Name of the Serial port to use")]
    public string PortName { get; set; }

    [Parameter("Timeout in Seconds", 30)] public int Timeout { get; set; } = 30;

    [Parameter("Baud Rate", 9600)] public int BaudRate { get; set; } = 9600;

    [Parameter("Number of Data Bits (Default: 8)", 8)]
    public int DataBits { get; set; } = 8;

    [Parameter("Data Parity (Default: None)", Parity.None)]
    public Parity Parity { get; set; } = Parity.None;

    [Parameter("Data Stop Bits (Default: None)", StopBits.None)]
    public StopBits StopBits { get; set; } = StopBits.None;

    [Parameter("NewLine Character (Default: None)", "")]
    public string NewLine { get; set; } = "";

    /// <summary>
    ///     Return a list of available port names that can be used by the adapter
    /// </summary>
    public static List<string> PortNames => SerialPort.GetPortNames().ToList();

    public abstract List<SerialAdapterSettings> ValidPorts   { get; }
    public abstract SerialAdapterSettings?      ValidSetting { get; }

    public bool IsConnected => _connection?.IsOpen ?? false;

    /// <summary>
    ///     Open a connection to this adapter if it is not already open (close it first if it is)
    /// </summary>
    public void Connect() {
        logger.Debug($"ADAPTER:{this.AttributeInfo().Name} - Connecting");
        if (IsConnected) Disconnect();

        if (string.IsNullOrEmpty(PortName)) {
            throw new AdapterException(this.AttributeInfo().Name, "No port has been defined. ");
        }

        try {
            _connection = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits) { WriteTimeout = Timeout, ReadTimeout = Timeout };
            if (!string.IsNullOrEmpty(NewLine)) _connection.NewLine = NewLine;

            //_connection.DataReceived += delegate (object sender, SerialDataReceivedEventArgs args) {
            //    Console.WriteLine($"{Name}: Received message: {0}", args.ToString());
            //    OnDataRecieved(new DataRecvArgs(args.ToString()!.ToByteArray(), this));
            //};

            //_connection.ErrorReceived += delegate(object sender, SerialErrorReceivedEventArgs args) {
            //    logger.Debug($"ADAPTER:{this.AttributeInfo().Name} - SerialConnection Error Occurred: {0}", args);
            //    OnErrorOccurred(new DataErrorArgs(args.EventType.ToString(), this));
            //};
            _connection.Open();
        } catch (Exception ex) {
            throw new AdapterException(this.AttributeInfo().Name, "Could not connect to the device: " + PortName, ex);
        }
    }

    /// <summary>
    ///     Close the open Serial connection and reset the reference
    /// </summary>
    public void Disconnect() {
        logger.Debug($"ADAPTER:{this.AttributeInfo().Name} - Disconnecting");
        if (_connection != null) _connection.Close();
        _connection = null;
    }

    /// <summary>
    ///     Reads a block of data from the Adapter and returns it as an array of bytes
    /// </summary>
    /// <param name="command">The command to associate with the received data</param>
    /// <returns>Array of Bytes being the data read from the Adapter</returns>
    /// <exception cref="AdapterException">Throws if there is a connection error</exception>
    public byte[]? RecvData(ICommand? command = null) {
        //logger.Debug($"ADAPTER:{this.AttributeInfo().Name} - Listening for data");

        if (!IsConnected) {
            throw new AdapterException(this.AttributeInfo().Name, "No active connection to the Command Station.");
        }

        GuardClauses.IsNotNull(_connection, "_connection");
        var returnData = new List<byte>();
        try {
            var readData = _connection?.ReadExisting() ?? "";

            //logger.Debug($"ADAPTER:{this.AttributeInfo().Name} -  Reading '{readData.Length}' data as bytes from SerialPort: '{readData}'");
            returnData.AddRange(readData.ToByteArray());
            OnDataRecieved(new DataRecvArgs(returnData.ToArray(), this, command));
            return returnData.ToArray();
        } catch (Exception ex) {
            throw new AdapterException(this.AttributeInfo().Name, "Could not read from the Command Station", ex);
        }
    }

    /// <summary>
    ///     Sends a block of data to the Adapter as an array of bytes
    /// </summary>
    /// <param name="data">The data stored as an array of bytes</param>
    /// <param name="commandReference">The reference to the command being sent</param>
    /// <exception cref="AdapterException">Throws an exception if there is a connection error</exception>
    public void SendData(string data, ICommand? commandReference = null) {
        SendData(data.ToByteArray(), commandReference);
    }

    public void SendData(byte[] data, ICommand? commandReference = null) {
        //logger.Debug($"ADAPTER:{this.AttributeInfo().Name} -Sending data to the {this.AttributeInfo().Name} Adapter '{data.ToDisplayValueChars()}'");

        if (!IsConnected) {
            throw new AdapterException(this.AttributeInfo().Name, "No active connection to the Command Station.");
        }

        try {
            if (_connection!.BytesToRead > 0) _connection.ReadExisting();
            _connection!.Write(data, 0, data.Length);
        } catch (Exception ex) {
            throw new AdapterException(this.AttributeInfo().Name, "Could not read/write to Command Station", ex);
        }

        OnDataSent(new DataSentArgs(data, this, commandReference));
    }

    /// <summary>
    ///     Dispose of the SerialAdapter and release any resources used.
    /// </summary>
    /// <remarks>
    ///     This method should be called when the SerialAdapter is no longer needed. It closes the open Serial connection
    ///     and releases any resources used by the adapter.
    /// </remarks>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this); // Violates rule
    }

    public abstract void ConfigureValidSettings();

    /// <summary>
    ///     Override the ToString to display "Serial = tty @ 9600,8,1,N"
    /// </summary>
    /// <returns>String representation of the connection string</returns>
    public override string ToString() {
        return $"Adapter '{this.AttributeInfo().Name}' = {PortName} @ {BaudRate},{DataBits},{StopBits},{Parity}";
    }

    /// <summary>
    ///     Disposes the SerialAdapter instance by closing the open Serial connection and releasing any resources.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose of managed resources.</param>
    protected virtual void Dispose(bool disposing) {
        if (disposing) {
            if (_connection != null) {
                Disconnect();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}