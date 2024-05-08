using System;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Virtual.Adapters;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualClockRead : VirtualCommand, ICmdClockRead, ICommand {
    public override ICmdResult Execute(IAdapter adapter) {

        // How long has it been since the reference time?
        // ---------------------------------------------------------------
        var result = new CmdResultFastClock(DateTime.Now);
        if (adapter is VirtualAdapter virtualAdapter) {
            var timeSinceReference = DateTime.Now - virtualAdapter.FastClockSetTime;
            result.CurrentTime = virtualAdapter.FastClockSetTime.AddTicks(timeSinceReference.Ticks * virtualAdapter.FastClockRatio);
        }
        return result;
    }

    public override string ToString() => "READ CLOCK";
}