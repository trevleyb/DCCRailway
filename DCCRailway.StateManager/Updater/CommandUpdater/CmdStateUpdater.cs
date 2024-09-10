using DCCRailway.Common.Result;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Controllers.Events;

namespace DCCRailway.StateManager.Updater.CommandUpdater;

/// <summary>
///     The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
///     and the Entities Configuration itself.This is because, while the layout might issue a command
///     to the command statin and it will be executed, other systems might issue a command also
///     and so if these are detected (for example if we have a DC packet analyser listening to commands)
///     then we need to update the Entities data with these changes.
///     So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
///     the collection of all data related to the current executing layout.
/// </summary>
public static class CmdStateUpdater {
    public static IResult Process(ControllerEventArgs message, IStateTracker stateTracker) {
        if (message is ControllerEventArgs { } eventArgs) {
            switch (eventArgs) {
            case CommandEventArgs args:

                // If the command failed, log the error and return.
                // -------------------------------------------------
                if (args.Result is { Success: false } failedResult) {
                    return Result.Fail(failedResult.Message);
                }

                // If the command was successful, process the command.
                // ---------------------------------------------------
                if (args.Result is { Success: true, Command: not null } cmdResult) {
                    var result = cmdResult.Command switch {
                        IAccyCmd    => new CmdStateUpdaterAccyCmd(stateTracker).Process(cmdResult),
                        ILocoCmd    => new CmdStateUpdaterLocoCmd(stateTracker).Process(cmdResult),
                        ISensorCmd  => new CmdStateUpdaterSensorCmd(stateTracker).Process(cmdResult),
                        ISignalCmd  => new CmdStateUpdaterSignalCmd(stateTracker).Process(cmdResult),
                        ISystemCmd  => new CmdStateUpdaterSystemCmd(stateTracker).Process(cmdResult),
                        IConsistCmd => new CmdStateUpdaterConsistCmd(stateTracker).Process(cmdResult),
                        ICVCmd      => new CmdStateUpdaterCvCmd(stateTracker).Process(cmdResult),
                        _           => new CmdStateUpdaterGenericCmd(stateTracker).Process(cmdResult)
                    };

                    return result ?? Result.Ok();
                }

                return Result.Ok();

            case AdapterEventArgs exec:
                //if (exec.Adapter != null) {
                //    logger.Error($"Command {exec.Adapter.AttributeInfo().Name} {exec.AdapterEvent} the commandStation.");
                // }
                return Result.Ok();

            default:
                return Result.Ok();
            }
        }

        return Result.Fail("Unknown Event Type.");
    }
}