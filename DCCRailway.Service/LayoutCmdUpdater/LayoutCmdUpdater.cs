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
public class LayoutCmdUpdater() {

    public void ProcessCommandEvent(ControllerEventArgs eventArgs) {
        switch (eventArgs) {
        case CommandEventArgs exec:

            // If the command failed, log the error and return.
            // -------------------------------------------------
            if (exec.Result is { IsFailure: true }) {
                if (exec.Command != null) Logger.Log.Information($"Command {exec.Command.AttributeInfo().Name} failed with error {exec.Result.Error}");
                return;
            }

            // If the command was successful, process the command.
            // ---------------------------------------------------
            if (exec.Command != null) {
                _ = exec.Command switch {
                    IAccyCmd cmd    => new LayoutAccyCmdUpdater().Process(exec.Command),
                    ILocoCmd cmd    => new LayoutLocoCmdUpdater().Process(exec.Command),
                    ISensorCmd cmd  => new LayoutSensorCmdUpdater().Process(exec.Command),
                    ISignalCmd cmd  => new LayoutSignalCmdUpdater().Process(exec.Command),
                    ISystemCmd cmd  => new LayoutSystemCmdUpdater().Process(exec.Command),
                    IConsistCmd cmd => new LayoutConsistCmdUpdater().Process(exec.Command),
                    ICVCmd cmd      => new LayoutCvCmdUpdater().Process(exec.Command),
                    _               => new LayoutGenericCmdUpdater().Process(exec.Command)
                };
            }
            break;

        case AdapterEventArgs exec:
            if (exec.Adapter != null) Logger.Log.Error($"Command {exec.Adapter.AttributeInfo().Name} {exec.AdapterEvent} the controller.");
            break;

        default:
            Logger.Log.Error($"Controller Evenet Raise: {eventArgs.Message}");
            break;
        }
    }
}