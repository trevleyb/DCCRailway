using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Events;

namespace DCCRailway.Core {
	public abstract class SystemBase : ISystem {
		private IAdapter? _adapter; // Stores the adapter to be used
		private Dictionary<Type, (Type Command, string Name)> _commands = new(); // Stores what operations the system will provide

		/// <summary>
		///     Execute a Given Command. We do this here so we can manage and log all command being executed
		///     and the results that each command recieved.
		/// </summary>
		/// <param name="command">The command object to be executed</param>
		/// <returns>A result object of type IResult which should be cast according to the command</returns>
		/// <exception cref="ApplicationException">Will throw an exception if no adapter specified</exception>
		public IResult? Execute(ICommand command) {
			if (_adapter == null) throw new ApplicationException("No Adapter has been provided.");
			return command.Execute(_adapter);
		}

		#region Supported Adapters List. Must be overriden to list supported Adapters
		/// <summary>
		///     Return a list of what Commands this system will support.
		/// </summary>
		public abstract List<(Type adapter, string name)>? SupportedAdapters { get; }
		#endregion

		protected abstract void Adapter_ErrorOccurred(object? sender, ErrorArgs e);
		protected abstract void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e);
		protected abstract void Adapter_DataSent(object? sender, DataSentArgs e);
		protected abstract void Adapter_DataReceived(object? sender, DataRecvArgs e);

		#region Create an Address. Must be overriden to the supported Command Station
		public abstract IDCCAddress CreateAddress();
		public abstract IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);
		#endregion

		#region Create and attach an Adapter to this system
		/// <summary>
		///     Property for the Adapter that this system will communicate through
		/// </summary>
		public IAdapter? Adapter {
			get { return _adapter; }
			set {
				if (_adapter != value) Detatch();
				Attach(value);
			}
		}

		/// <summary>
		///     Create and return an Adapter. This does not attach it. This command should be executed as
		///     System.Adapter = System.CreateAdapter(name);
		/// </summary>
		public IAdapter? CreateAdapter<T>() where T : IAdapter {
			if (SupportedAdapters != null && SupportedAdapters.Count > 0) {
				foreach (var (adapter, _) in SupportedAdapters!) {
					if (adapter == typeof(T)) return (IAdapter?)Activator.CreateInstance(adapter);
				}
			}
			return null;
		}

		/// <summary>
		///     Create and return an Adapter. This does not attach it. This command should be executed as
		///     System.Adapter = System.CreateAdapter(name);
		/// </summary>
		public IAdapter? CreateAdapter(string adapterName) {
			if (SupportedAdapters != null && SupportedAdapters.Count > 0) {
				foreach (var (adapter, name) in SupportedAdapters!) {
					if (name.Equals(adapterName, StringComparison.InvariantCultureIgnoreCase)) return (IAdapter?)Activator.CreateInstance(adapter);
				}
			}
			return null;
		}

		/// <summary>
		///     Remove an previously attached adapter. Ensure it is closed and resources returned
		/// </summary>
		public void Detatch() {
			if (_adapter != null) {
				_adapter.DataReceived -= Adapter_DataReceived;
				_adapter.DataSent -= Adapter_DataSent;
				_adapter.ErrorOccurred -= Adapter_ErrorOccurred;
				_adapter.ConnectionStatusChanged -= Adapter_ConnectionStatusChanged;
				_adapter.Disconnect();
				_commands = new Dictionary<Type, (Type command, string name)>();
			}
		}

		public IAdapter? Attach(IAdapter? adapter) {
			if (adapter != null) {
				_adapter = adapter;
				_adapter.DataReceived += Adapter_DataReceived;
				_adapter.DataSent += Adapter_DataSent;
				_adapter.ErrorOccurred += Adapter_ErrorOccurred;
				_adapter.ConnectionStatusChanged += Adapter_ConnectionStatusChanged;
				_adapter.Connect();
				RegisterCommands();
			}
			return adapter;
		}
		#endregion

		#region Generic Factory Implementation to register what operations each System will provide
		/// <summary>
		///     This function must be overridden in the derived class to ensure that the operations
		///     that this system provides are loaded. This is done because some operations may only
		///     be supported on a particular adapter or interface (eg: NCE does not support the clock
		///     functions if using the USBSerial adapter).
		/// </summary>
		protected abstract void RegisterCommands();

		/// <summary>
		///     Register what commands are suitable for the system (what it supports)
		///     For example, the PowerCab does not support the CLOCK functions via
		///     the USB Serial interface.
		///     Call Register
		///     <T>
		///         to register a Command
		///         Call UnRegister<T> to remove a previous registered command
		/// </summary>
		protected void Register<T>(Type command) where T : ICommand {
			if (command == null) throw new ApplicationException("Command instance cannot be NULL and must be a concrete object.");

			if (!_commands.ContainsKey(typeof(T))) _commands.TryAdd(typeof(T), (command, command.Name));
		}

		protected void UnRegister<T>() where T : ICommand {
			if (_commands.ContainsKey(typeof(T))) _commands.Remove(typeof(T));
		}

		/// <summary>
		///     Returns true if the command request is supported by this system and adpater
		/// </summary>
		/// <typeparam name="T">The command type to check</typeparam>
		/// <returns>True if it is supported, false otherwise</returns>
		public bool IsCommandSupported<T>() where T : ICommand {
			foreach (KeyValuePair<Type, (Type command, string name)> entry in _commands) {
				if (entry.Key == typeof(T)) return true;
			}

			return false;
		}

		/// <summary>
		///     Return a list of what Commands this system will support.
		///     This is only valid AFTER an adapter has been associated with the System
		/// </summary>
		public List<(Type command, string name)>? SupportedCommands {
			get { return _commands.Values.ToList(); }
		}
		#endregion

		#region Create Command Objects that are supported by this System.
		/// <summary>
		///     Creates a command object that can be executed. A command object can be
		///     executed by calling its execute function or passing it back to the
		///     system to ask it to execute it.
		/// </summary>
		/// <typeparam name="T">An interface that adheres to a ICommand interface</typeparam>
		/// <returns>An object instance that is a Type T object</returns>
		public TCommand? CreateCommand<TCommand>() where TCommand : ICommand {
			if (_adapter == null) throw new ApplicationException("Adapter cannot be null when creating commands");

			var typeToCreate = _commands[typeof(TCommand)].Command ?? null;
			if (typeToCreate == null) throw new ApplicationException("Should not have an instance where the command returned is NULL");

			try {
				var command = (TCommand?)Activator.CreateInstance(typeToCreate, true);
				if (command == null) throw new ApplicationException("Could not create an instance of the command.");

				return command;
			} catch (Exception ex) {
				throw new ApplicationException("Could not create an instance of the command.", ex);
			}
		}

		public TCommand? CreateCommand<TCommand>(int value) where TCommand : ICommand {
			return Create<TCommand, int>(value);
		}

		public TCommand? CreateCommand<TCommand>(byte value) where TCommand : ICommand {
			return Create<TCommand, byte>(value);
		}

		private TCommand? Create<TCommand, P>(P value) where TCommand : ICommand {
			if (_adapter == null) throw new ApplicationException("Adapter cannot be null when creating commands");

			var typeToCreate = _commands[typeof(TCommand)].Command ?? null;
			if (typeToCreate == null) throw new ApplicationException("Should not have an instance where the command returned is NULL");

			try {
				var command = (TCommand?)Activator.CreateInstance(typeToCreate, value);
				if (command == null) throw new ApplicationException("Could not create an instance of the command.");

				return command;
			} catch (Exception ex) {
				throw new ApplicationException("Could not create an instance of the command.", ex);
			}
		}
		#endregion
	}
}