using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Layout.Updaters;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;

namespace DCCRailway.Railway.Layout;
/// <summary>
/// The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
/// and the Entities Configuration itself.This is because, while the layout might issue a command
/// to the command statin and it will be executed, other systems might issue a command also
/// and so if these are detected (for example if we have a DC packet analyser listening to commands)
/// then we need to update the Entities data with these changes.
///
/// So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
/// the collection of all data related to the current executing layout.
/// </summary>
public class LayoutUpdater {

    private readonly LayoutEventLogger _eventLogger = new LayoutEventLogger();

    public void ProcessCommandEvent(ControllerEventArgs eventArgs) {
        switch (eventArgs) {
        case CommandEventArgs exec:

            // If the command failed, log the error and return.
            // -------------------------------------------------
            if (exec.Result is { Success: false }) {
                if (exec.Command != null) Logger.Log.Information($"Command {exec.Command.AttributeInfo().Name} failed with error {exec.Result.ErrorMessage}");
                return;
            }

            // If the command was successful, process the command.
            // ---------------------------------------------------
            if (exec.Command != null) {
                _ = exec.Command switch {
                    IAccyCmd     => new LayoutAccyCmdUpdater().Process(exec.Command,_eventLogger),
                    ILocoCmd     => new LayoutLocoCmdUpdater().Process(exec.Command,_eventLogger),
                    ISensorCmd   => new LayoutSensorCmdUpdater().Process(exec.Command,_eventLogger),
                    ISignalCmd   => new LayoutSignalCmdUpdater().Process(exec.Command,_eventLogger),
                    ISystemCmd   => new LayoutSystemCmdUpdater().Process(exec.Command,_eventLogger),
                    IConsistCmd  => new LayoutConsistCmdUpdater().Process(exec.Command,_eventLogger),
                    ICVCmd       => new LayoutCvCmdUpdater().Process(exec.Command,_eventLogger),
                    _            => new LayoutGenericCmdUpdater().Process(exec.Command,_eventLogger)
                };
            }
            break;

        case AdapterEventArgs exec:
            if (exec.Adapter != null) Logger.Log.Error($"Command {exec.Adapter.AttributeInfo().Name} {exec.AdapterEvent} the commandStation.");
            break;

        default:
            Logger.Log.Error($"CommandStation Event Raise: {eventArgs.Message}");
            break;
        }
    }
}