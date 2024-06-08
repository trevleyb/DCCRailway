using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.StateManager.State;
using Serilog;

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
public class CmdStateUpdater(ILogger logger, IStateManager stateManager) : IStateUpdater {
    public IResult Process(object message) {
        if (message is ControllerEventArgs { } eventArgs) {
            switch (eventArgs) {
            case CommandEventArgs args:

                // If the command failed, log the error and return.
                // -------------------------------------------------
                if (args.Result is { Success: false } failedResult) {
                    logger.Information(failedResult.Command != null ? $"Command {failedResult.Command.AttributeInfo().Name} failed with error {failedResult.Message}" : $"Command 'unknown' failed with error {failedResult.Message}");
                    return Result.Fail(failedResult.Message);
                }

                // If the command was successful, process the command.
                // ---------------------------------------------------
                if (args.Result is { Success: true, Command: not null } cmdResult) {
                    var result = cmdResult.Command switch {
                        IAccyCmd    => new CmdStateUpdaterAccyCmd(stateManager).Process(cmdResult),
                        ILocoCmd    => new CmdStateUpdaterLocoCmd(stateManager).Process(cmdResult),
                        ISensorCmd  => new CmdStateUpdaterSensorCmd(stateManager).Process(cmdResult),
                        ISignalCmd  => new CmdStateUpdaterSignalCmd(stateManager).Process(cmdResult),
                        ISystemCmd  => new CmdStateUpdaterSystemCmd(stateManager).Process(cmdResult),
                        IConsistCmd => new CmdStateUpdaterConsistCmd(stateManager).Process(cmdResult),
                        ICVCmd      => new CmdStateUpdaterCvCmd(stateManager).Process(cmdResult),
                        _           => new CmdStateUpdaterGenericCmd(stateManager).Process(cmdResult)
                    };

                    logger.Information("Processed State Command: {0} with result {1} {2}.", cmdResult?.Command?.ToString(), result.Success ? "Success" : "Failed", string.IsNullOrEmpty(result?.Message) ? "" : $"and message '{result.Message}'");
                    return result ?? Result.Ok();
                }

                return Result.Ok();

            case AdapterEventArgs exec:
                //if (exec.Adapter != null) {
                //    logger.Error($"Command {exec.Adapter.AttributeInfo().Name} {exec.AdapterEvent} the commandStation.");
                // }
                return Result.Ok();

            default:
                logger.Verbose($"CommandStation Event Raise: {eventArgs.Message}");
                return Result.Ok();
            }
        }

        return Result.Fail("Unknown Event Type.");
    }
}