using System;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Virtual.Adapters;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("ReadClock", "Read the Clock from the Virtual CommandStation")]
public class VirtualClockRead : VirtualCommand, ICmdClockRead, ICommand {
    protected override ICmdResultFastClock Execute(IAdapter adapter) {
        // How long has it been since the reference time?
        // ---------------------------------------------------------------
        var result = new CmdResultFastClock {
            CurrentTime = DateTime.Now
        };
        if (adapter is VirtualAdapter virtualAdapter) {
            var timeSinceReference = DateTime.Now - virtualAdapter.FastClockSetTime;
            result.CurrentTime = virtualAdapter.FastClockSetTime.AddTicks(timeSinceReference.Ticks * virtualAdapter.FastClockRatio);
        }
        return result;
    }

    public override string ToString() => "READ CLOCK";
}