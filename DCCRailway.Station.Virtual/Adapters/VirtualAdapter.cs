using System;
using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;

namespace DCCRailway.Station.Virtual.Adapters;

[Adapter("Virtual", AdapterType.Virtual)]
public class VirtualAdapter : ConsoleAdapter, IAdapter {

    public DCCPowerState PowerState = DCCPowerState.Unknown;

    public DateTime FastClockSetTime;
    public int FastClockRatio = 1;
    public bool FastClockState = false;
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