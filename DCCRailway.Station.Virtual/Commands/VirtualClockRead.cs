using System;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Virtual.Adapters;

namespace DCCRailway.Station.Virtual.Commands;

[Command("ReadClock", "Read the Clock from the Virtual Controller")]
public class VirtualClockRead : VirtualCommand, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {

        // How long has it been since the reference time?
        // ---------------------------------------------------------------
        var result = new CommandResultFastClock(DateTime.Now);
        if (adapter is VirtualAdapter virtualAdapter) {
            var timeSinceReference = DateTime.Now - virtualAdapter.FastClockSetTime;
            result.CurrentTime = virtualAdapter.FastClockSetTime.AddTicks(timeSinceReference.Ticks * virtualAdapter.FastClockRatio);
        }
        return result;
    }

    public override string ToString() => "READ CLOCK";
}