using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Simulator;

namespace DCCRailway.Core.Adapters {
	/// <summary>
	///     A virtual adapter connects to a simulator and accepts simulated commands. Based on the SEND/RECV it is passed
	///     through
	///     and the simulator will act like a command station and will track the locos and accessories that are communicated
	///     with.
	/// </summary>
	public abstract class VirtualAdapter : BaseAdapter, IAdapter {
		private object? _lastResult;
		private DCCSimulator? _simulator;

		public static string Name {
			get { return "Virtual Adapter"; }
		}

		public bool IsConnected {
			get { return _simulator != null; }
		}

		/// <summary>
		///     When connecting, create a new Simulator Instance Class
		/// </summary>
		public void Connect() {
			if (IsConnected && _simulator != null) Disconnect();
			_simulator = new DCCSimulator();
		}

		/// <summary>
		///     Disconnect from the Simulator. Clear it and release any memory used.
		/// </summary>
		public void Disconnect() {
			_simulator = null;
		}

		/// <summary>
		///     Send a command to the simulator but also allow an override in a simulator instance
		///     for specific results to be returned (for example, returning a simulator version number)
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public byte[]? RecvData(ICommand command) {
			return MapSimulatorResult(_lastResult, command);
		}

		public void SendData(byte[] data, ICommand command) {
			if (!IsConnected) throw new AdapterException(this, "Not connected to the simulator.");
			if (command == null) throw new AdapterException(this, "No command actually specified in Simulator. Aborting");

			_lastResult = command.GetType() switch {
				ICmdPowerSetOn => _simulator!.SetPowerState(true),
				ICmdPowerSetOff => _simulator!.SetPowerState(false),
				ICmdPowerGetState => _simulator!.GetPowerState(),
				ICmdTrackMain => _simulator!.SetMainTrack(),
				ICmdTrackProg => _simulator!.SetProgTrack(),
				ICmdConsistCreate => _simulator!.CreateConsist((DCCAddress)((ICmdConsistCreate)command).LeadLoco.Address, ((ICmdConsistCreate)command).LeadLoco.Direction, (DCCAddress)((ICmdConsistCreate)command).RearLoco.Address,
					((ICmdConsistCreate)command).RearLoco.Direction),
				ICmdConsistKill => _simulator!.KillConsist((DCCAddress)((ICmdConsistKill)command).Address),
				ICmdConsistAdd => _simulator!.AddConsist(((ICmdConsistAdd)command).ConsistAddress, (DCCAddress)((ICmdConsistAdd)command).Loco.Address, ((ICmdConsistAdd)command).Loco.Direction),
				ICmdConsistDelete => _simulator!.DelConsist((DCCAddress)((ICmdConsistDelete)command).Address),
				ICmdClockSet => _simulator!.SetClock(((ICmdClockSet)command).Hour, ((ICmdClockSet)command).Minute, ((ICmdClockSet)command).Ratio),
				ICmdClockRead => _simulator!.ReadClock(),
				ICmdClockStart => _simulator!.StartClock(),
				ICmdClockStop => _simulator!.StopClock(),
				ICmdCVRead => _simulator!.ReadCV(((ICmdCVRead)command).CV),
				ICmdCVWrite => _simulator!.WriteCV(((ICmdCVWrite)command).CV, ((ICmdCVWrite)command).Value),
				ICmdLocoOpsProg => _simulator!.LocoOpsProgramming((DCCAddress)((ICmdLocoOpsProg)command).LocoAddress, ((ICmdLocoOpsProg)command).CVAddress.Address, ((ICmdLocoOpsProg)command).Value),
				ICmdAccyOpsProg => _simulator!.AccyOpsProgramming((DCCAddress)((ICmdAccyOpsProg)command).LocoAddress, ((ICmdLocoOpsProg)command).CVAddress.Address, ((ICmdLocoOpsProg)command).Value),
				ICmdSignalSetAspect => _simulator!.SetSignalAspect((DCCAddress)((ICmdSignalSetAspect)command).Address, ((ICmdSignalSetAspect)command).Aspect),
				ICmdAccySetState => _simulator!.SetAccyState((DCCAddress)((ICmdAccySetState)command).Address, ((ICmdAccySetState)command).State),
				ICmdStatus => _simulator!.GetStatus(),
				ICmdMacroRun => _simulator!.RunMacro(((ICmdMacroRun)command).Macro),
				ICmdLocoStop => _simulator!.StopLoco((DCCAddress)((ICmdLocoStop)command).Address),
				ICmdLocoSetFunctions => _simulator!.SetLocoFunctions((DCCAddress)((ICmdLocoSetFunctions)command).Address, ((ICmdLocoSetFunctions)command).Functions),
				ICmdLocoSetSpeed => _simulator!.SetLocoSpeed((DCCAddress)((ICmdLocoSetSpeed)command).Address, ((ICmdLocoSetSpeed)command).Speed, ((ICmdLocoSetSpeed)command).Direction),
				ICmdLocoSetSpeedSteps => _simulator!.SetLocoSpeedSteps((DCCAddress)((ICmdLocoSetSpeedSteps)command).Address, ((ICmdLocoSetSpeedSteps)command).SpeedSteps),
				ICmdLocoSetMomentum => _simulator!.SetLocoMomentum((DCCAddress)((ICmdLocoSetMomentum)command).Address, ((ICmdLocoSetMomentum)command).Momentum),
				IDummyCmd => _simulator!.DoNothing(),
				_ => null
			};
		}

		protected abstract byte[]? MapSimulatorResult(object? lastResult, ICommand command);
	}
}