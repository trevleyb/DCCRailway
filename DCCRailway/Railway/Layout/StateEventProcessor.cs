using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Railway.Layout.Processors;
using DCCRailway.Railway.Layout.State;
using Serilog;

namespace DCCRailway.Railway.Layout;

/// <summary>
///     The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
///     and the Entities Configuration itself.This is because, while the layout might issue a command
///     to the command statin and it will be executed, other systems might issue a command also
///     and so if these are detected (for example if we have a DC packet analyser listening to commands)
///     then we need to update the Entities data with these changes.
///     So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
///     the collection of all data related to the current executing layout.
/// </summary>
public class StateEventProcessor(ILogger logger, IRailwayManager railwayManager, IStateManager stateManager) {
    public void ProcessCommandEvent(ControllerEventArgs eventArgs) {
        switch (eventArgs) {
        case CommandEventArgs args:

            // If the command failed, log the error and return.
            // -------------------------------------------------
            if (args.Result is { Success: false } failedResult) {
                logger.ForContext<StateEventProcessor>().Information(failedResult.Command != null ? $"Command {failedResult.Command.AttributeInfo().Name} failed with error {failedResult.ErrorMessage}" : $"Command 'unknown' failed with error {failedResult.ErrorMessage}");
                return;
            }

            // If the command was successful, process the command.
            // ---------------------------------------------------
            if (args.Result is { Success: true, Command: not null } result) {
                IStateUpdaterProcess stateUpdater = result.Command switch {
                    IAccyCmd    => new StateUpdaterAccyCmd(railwayManager, stateManager, result),
                    ILocoCmd    => new StateUpdaterLocoCmd(railwayManager, stateManager, result),
                    ISensorCmd  => new StateUpdaterSensorCmd(railwayManager, stateManager, result),
                    ISignalCmd  => new StateUpdaterSignalCmd(railwayManager, stateManager, result),
                    ISystemCmd  => new StateUpdaterSystemCmd(railwayManager, stateManager, result),
                    IConsistCmd => new StateUpdaterConsistCmd(railwayManager, stateManager, result),
                    ICVCmd      => new StateUpdaterCvCmd(railwayManager, stateManager, result),
                    _           => new StateUpdaterGenericCmd(railwayManager, stateManager, result)
                };
                stateUpdater.Process();
            }
            break;

        case AdapterEventArgs exec:
            if (exec.Adapter != null) logger.ForContext<StateEventProcessor>().Error($"Command {exec.Adapter.AttributeInfo().Name} {exec.AdapterEvent} the commandStation.");
            break;

        default:
            logger.ForContext<StateEventProcessor>().Error($"CommandStation Event Raise: {eventArgs.Message}");
            break;
        }
    }
}