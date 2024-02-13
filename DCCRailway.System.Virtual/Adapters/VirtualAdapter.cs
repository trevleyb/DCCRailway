using System.Dynamic;
using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Adapters.Events;
using DCCRailway.System.Layout.Commands;
using DCCRailway.System.Simulator;
using DCCRailway.System.Utilities;
using DCCRailway.System.Utilities.Exceptions;

namespace DCCRailway.System.Virtual.Adapters;

[Adapter("Virtual", AdapterType.Virtual)]
public abstract class VirtualAdapter : Adapter, IAdapter {
    private object? _lastResult;
    private byte[]  _lastCommand;
    private DCCSimulator? _simulator;

    public bool IsConnected { get; set; }

    /// <summary>
    ///     When connecting, create a new Simulator Instance Class
    /// </summary>
    public void Connect() {
        Logger.Log.Debug("Connecting to the Virtual Adapter");
        if (IsConnected && _simulator != null) Disconnect();
        _simulator = new DCCSimulator();
        IsConnected = true;
    }

    /// <summary>
    ///     Disconnect from the Simulator. Clear it and release any memory used.
    /// </summary>
    public void Disconnect() {
        Logger.Log.Debug("Disconnecting from the Virtual Adapter");
        _simulator = null;
        IsConnected = false;
    }

    /// <summary>
    ///     Send a command to the simulator but also allow an override in a simulator instance
    ///     for specific results to be returned (for example, returning a simulator version number)
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public byte[]? RecvData(ICommand command) {
        Logger.Log.Debug("Listening for data from the Adapter: '" + _lastCommand.FromByteArray() + "'");
        //var result = MapSimulatorResult(_lastResult, command);
        //OnDataRecieved(new DataRecvArgs(result, this, command));
        return new[] { (byte)0x00 };
    }

    public void SendData(byte[] data, ICommand command) {
        Logger.Log.Debug("Sending data to the Adapter");
        if (!IsConnected) throw new AdapterException(this, "Not connected to the simulator.");
        if (command == null) throw new AdapterException(this, "No command actually specified in Simulator. Aborting");

        _lastCommand = data;
        OnDataSent(new DataSentArgs(data, this, command));

        /*
        _lastResult = command.GetType() switch {
            ICmdPowerSetOn        => _simulator!.SetPowerState(true),
            ICmdPowerSetOff       => _simulator!.SetPowerState(false),
            ICmdPowerGetState     => _simulator!.GetPowerState(),
            ICmdTrackMain         => _simulator!.SetMainTrack(),
            ICmdTrackProg         => _simulator!.SetProgTrack(),
            ICmdConsistCreate     => _simulator!.CreateConsist((DCCAddress)((ICmdConsistCreate)command).LeadLoco.Address, ((ICmdConsistCreate)command).LeadLoco.Direction, (DCCAddress)((ICmdConsistCreate)command).RearLoco.Address, ((ICmdConsistCreate)command).RearLoco.Direction),
            ICmdConsistKill       => _simulator!.KillConsist((DCCAddress)((ICmdConsistKill)command).Address),
            ICmdConsistAdd        => _simulator!.AddConsist(((ICmdConsistAdd)command).ConsistAddress, (DCCAddress)((ICmdConsistAdd)command).Loco.Address, ((ICmdConsistAdd)command).Loco.Direction),
            ICmdConsistDelete     => _simulator!.DelConsist((DCCAddress)((ICmdConsistDelete)command).Address),
            ICmdClockSet          => _simulator!.SetClock(((ICmdClockSet)command).Hour, ((ICmdClockSet)command).Minute, ((ICmdClockSet)command).Ratio),
            ICmdClockRead         => _simulator!.ReadClock(),
            ICmdClockStart        => _simulator!.StartClock(),
            ICmdClockStop         => _simulator!.StopClock(),
            ICmdCVRead            => _simulator!.ReadCV(((ICmdCVRead)command).CV),
            ICmdCVWrite           => _simulator!.WriteCV(((ICmdCVWrite)command).CV, ((ICmdCVWrite)command).Value),
            ICmdLocoOpsProg       => _simulator!.LocoOpsProgramming((DCCAddress)((ICmdLocoOpsProg)command).LocoAddress, ((ICmdLocoOpsProg)command).Address.Address, ((ICmdLocoOpsProg)command).Value),
            ICmdAccyOpsProg       => _simulator!.AccyOpsProgramming((DCCAddress)((ICmdAccyOpsProg)command).LocoAddress, ((ICmdLocoOpsProg)command).Address.Address, ((ICmdLocoOpsProg)command).Value),
            ICmdSignalSetAspect   => _simulator!.SetSignalAspect((DCCAddress)((ICmdSignalSetAspect)command).Address, ((ICmdSignalSetAspect)command).Aspect),
            ICmdAccySetState      => _simulator!.SetAccyState((DCCAddress)((ICmdAccySetState)command).Address, ((ICmdAccySetState)command).State),
            ICmdStatus            => _simulator!.GetStatus(),
            ICmdMacroRun          => _simulator!.RunMacro(((ICmdMacroRun)command).Macro),
            ICmdLocoStop          => _simulator!.StopLoco((DCCAddress)((ICmdLocoStop)command).Address),
            ICmdLocoSetFunctions  => _simulator!.SetLocoFunctions((DCCAddress)((ICmdLocoSetFunctions)command).Address, ((ICmdLocoSetFunctions)command).Functions),
            ICmdLocoSetSpeed      => _simulator!.SetLocoSpeed((DCCAddress)((ICmdLocoSetSpeed)command).Address, ((ICmdLocoSetSpeed)command).Speed, ((ICmdLocoSetSpeed)command).Direction),
            ICmdLocoSetSpeedSteps => _simulator!.SetLocoSpeedSteps((DCCAddress)((ICmdLocoSetSpeedSteps)command).Address, ((ICmdLocoSetSpeedSteps)command).SpeedSteps),
            ICmdLocoSetMomentum   => _simulator!.SetLocoMomentum((DCCAddress)((ICmdLocoSetMomentum)command).Address, ((ICmdLocoSetMomentum)command).Momentum),
            IDummyCmd             => _simulator!.DoNothing(),
            _                     => null
        };
        */
    }

}