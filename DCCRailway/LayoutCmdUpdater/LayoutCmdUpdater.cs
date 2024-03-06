using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types.Base;
using DCCRailway.System.Controllers.Events;

namespace DCCRailway.LayoutCmdUpdater;

public class LayoutCmdUpdater(DCCRailwayConfig config) {
    public void ProcessCommandEvent(ControllerEventArgs eventArgs) {
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
                IAccyCmd cmd    => new LayoutAccyCmdUpdaterUpdater(config).Process(exec.Command),
                ILocoCmd cmd    => new LayoutLocoCmdUpdater(config).Process(exec.Command),
                ISensorCmd cmd  => new LayoutSensorCmdUpdater(config).Process(exec.Command),
                ISignalCmd cmd  => new LayoutSignalCmdUpdater(config).Process(exec.Command),
                ISystemCmd cmd  => new LayoutSystemCmdUpdater(config).Process(exec.Command),
                IConsistCmd cmd => new LayoutConsistCmdUpdaterUpdater(config).Process(exec.Command),
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