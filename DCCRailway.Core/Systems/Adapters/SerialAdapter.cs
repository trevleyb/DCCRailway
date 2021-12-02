using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Systems.Adapters.Events;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Core.Systems.Adapters {
	public abstract class SerialAdapter : BaseAdapter, IAdapter, IDisposable {
		/// <summary>
		///     Return a list of available port names that can be used by the adapter
		/// </summary>
		public static List<string> PortNames {
			get { return SerialPort.GetPortNames().ToList(); }
		}

		public static string Name {
			get { return "Serial Adapter"; }
		}

		public bool IsConnected {
			get { return _connection?.IsOpen ?? false; }
		}

		/// <summary>
		///     Open a connection to this adapter if it is not already open (close it first if it is)
		/// </summary>
		public void Connect() {
			Logger.Log.Debug($"ADAPTER:{Name} - Connecting");

			if (IsConnected) Disconnect();
			if (string.IsNullOrEmpty(_portName)) throw new AdapterException(Name, "No port has been defined. ");

			try {
				_connection = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits) {
					WriteTimeout = _timeout,
					ReadTimeout = _timeout
				};

				_connection.PinChanged += delegate(object sender, SerialPinChangedEventArgs args) {
					Logger.Log.Debug($"ADAPTER:{Name} - Connected = {0}", args.EventType);
					OnConnectionChangedState(new StateChangedArgs(args.EventType.ToString()));
				};

				//_connection.DataReceived += delegate (object sender, SerialDataReceivedEventArgs args) {
				//    Console.WriteLine($"{Name}: Received message: {0}", args.ToString());
				//    OnDataRecieved(new DataRecvArgs(args.ToString()!.ToByteArray(), this));
				//};

				_connection.ErrorReceived += delegate(object sender, SerialErrorReceivedEventArgs args) {
					Logger.Log.Debug($"ADAPTER:{Name} - SerialConnection Error Occurred: {0}", args);
					OnErrorOccurred(new ErrorArgs(args.EventType.ToString(), this));
				};

				_connection.Open();
			} catch (Exception ex) {
				throw new AdapterException(Name, "Could not connect to the device: " + _portName, ex);
			}
		}

		/// <summary>
		///     Close the open Serial connection and reset the reference
		/// </summary>
		public void Disconnect() {
			Logger.Log.Debug($"ADAPTER:{Name} - Disconnecting");
			if (_connection != null) _connection.Close();
			_connection = null;
		}

		/// <summary>
		///     Reads a block of data from the Adapater and returns it as an
		///     array of bytes
		/// </summary>
		/// <returns>Array of Bytes being the data read from the Adapter</returns>
		/// <exception cref="AdapterException">Throws if there is a connection error</exception>
		public byte[]? RecvData(ICommand? command = null) {
			Logger.Log.Debug($"ADAPTER:{Name} - Listening for data");
			if (!IsConnected) throw new AdapterException(Name, "No active connection to the Command Station.");

			GuardClauses.IsNotNull(_connection, "_connection");
			try {
				var timeoutTime = DateTime.Now.AddMilliseconds(_timeout);
				var returnData = new List<byte>();
				var readBytes = true;

				while (DateTime.Now < timeoutTime && (readBytes || _connection!.BytesToRead > 0)) {
					if (_connection!.BytesToRead > 0) {
						readBytes = false;
						var readData = new byte[_connection.BytesToRead];
						_connection.Read(readData, 0, _connection.BytesToRead);

						Logger.Log.Debug($"ADAPTER:{Name} -  Reading '{readData.Length}' data as bytes from SerialPort: '{readData.ToDisplayValueChars()}'");
						returnData.AddRange(readData);
					}
				}

				Logger.Log.Debug($"ADAPTER:{Name} - Read '{0}' data as bytes from SerialPort.", returnData.ToArray().ToDisplayValueChars());
				OnDataRecieved(new DataRecvArgs(returnData.ToArray(), this, command));
				return returnData.ToArray();
			} catch (Exception ex) {
				throw new AdapterException(Name, "Could not read from the Command Station", ex);
			}
		}

		/// <summary>
		///     Sends a block of data to the Adapter as an array of bytes
		/// </summary>
		/// <param name="data">The data storerd as an array of bytes</param>
		/// <exception cref="AdapterException">Throws an exception if there is a connection error</exception>
		public void SendData(byte[] data, ICommand? commandReference = null) {
			Logger.Log.Debug($"ADAPTER:{Name} -Sending data to the {Name} Adapter '{data.ToDisplayValueChars()}'");
			if (!IsConnected) throw new AdapterException(Name, "No active connection to the Command Station.");

			try {
				if (_connection!.BytesToRead > 0) _connection.ReadExisting();
				_connection!.Write(data, 0, data.Length);
			} catch (Exception ex) {
				throw new AdapterException(Name, "Could not read/write to Command Station", ex);
			}
			OnDataSent(new DataSentArgs(data, this, commandReference));
		}

		/// <summary>
		///     Override the ToString to display "Serial = tty @ 9600,8,1,N"
		/// </summary>
		/// <returns>String representation of the connection string</returns>
		public override string ToString() {
			return $"Adapter '{Name}' = {_portName} @ {_baudRate},{_dataBits},{_stopBits},{_parity}";
		}

		#region local private data to hold connection information
		private readonly string _portName;
		private readonly int _timeout;
		private readonly int _baudRate;
		private readonly int _dataBits;
		private readonly Parity _parity;
		private readonly StopBits _stopBits;
		private SerialPort? _connection;
		#endregion

		#region Constructor and Destructor
		public SerialAdapter(string portName = "dev/ttyUSB0", int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int timeout = 2000) {
			_portName = portName;
			_baudRate = baudRate;
			_dataBits = dataBits;
			_parity = parity;
			_stopBits = stopBits;
			_timeout = timeout;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this); // Violates rule
		}

		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				if (_connection != null) {
					Disconnect();
					_connection.Dispose();
					_connection = null;
				}
			}
		}
		#endregion
	}
}