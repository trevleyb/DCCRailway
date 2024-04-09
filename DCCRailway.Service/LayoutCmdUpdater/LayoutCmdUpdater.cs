using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types.Base;
using DCCRailway.System.Controllers.Events;

namespace DCCRailway.LayoutCmdUpdater;
/// <summary>
/// The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
/// and the Layout Configuration itself.This is because, while the layout might issue a command
/// to the command statin and it will be executed, other systems might issue a command also
/// and so if these are detected (for example if we have a DC packet analyser listening to commands)
/// then we need to update the Layout data with these changes.
///
/// So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
/// the collection of all data related to the current executing layout.
/// </summary>
/// <param name="config"></param>
public class LayoutCmdUpdater(DCCRailwayConfig config) {
    public void ProcessCommandEvent(IControllerEventArgs eventArgs) {
        switch (eventArgs) {
        case ControllerEventCommandExec exec:

            // If the command failed, log the error and return.
            // -------------------------------------------------
            if (exec.Result.IsFailure) {
                Logger.Log.Information($"Command {exec.Command.AttributeInfo().Name} failed with error {exec.Result.Error}");
                return;
            }

            // If the command was successful, process the command.
            // ---------------------------------------------------
            _ = exec.Command switch {
                IAccyCmd cmd    => new LayoutAccyCmdUpdater(config).Process(exec.Command),
                ILocoCmd cmd    => new LayoutLocoCmdUpdater(config).Process(exec.Command),
                ISensorCmd cmd  => new LayoutSensorCmdUpdater(config).Process(exec.Command),
                ISignalCmd cmd  => new LayoutSignalCmdUpdater(config).Process(exec.Command),
                ISystemCmd cmd  => new LayoutSystemCmdUpdater(config).Process(exec.Command),
                IConsistCmd cmd => new LayoutConsistCmdUpdater(config).Process(exec.Command),
                ICVCmd cmd      => new LayoutCvCmdUpdater(config).Process(exec.Command),
                _               => new LayoutGenericCmdUpdater(config).Process(exec.Command)
            };

            break;

        case ControllerEventAdapterAdd exec:
            Logger.Log.Error($"Command {exec.Adapter.AttributeInfo().Name} added to the controller.");

            break;

        case ControllerEventAdapterDel exec:
            Logger.Log.Error($"Command {exec.Adapter.AttributeInfo().Name} removed from the controller.");

            break;

        case ControllerEventAdapter exec:
            Logger.Log.Error($"Command {exec.Adapter.AttributeInfo().Name} executed {exec.AdapterEvent?.Command?.AttributeInfo().Name}");

            break;

        default:
            throw new Exception("Unexpected type of event raised.");
        }
    }
}