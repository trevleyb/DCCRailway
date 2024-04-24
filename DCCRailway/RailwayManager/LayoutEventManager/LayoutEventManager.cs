using DCCRailway.Common.Utilities;
using DCCRailway.LayoutEventUpdater.Updaters;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types.Base;
using DCCRailway.Station.Controllers.Events;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace DCCRailway.LayoutEventUpdater;
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
public class LayoutEventManager {

    private LayoutEventLogger _eventLogger = new LayoutEventLogger();

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
                    IAccyCmd cmd    => new LayoutAccyCmdUpdater().Process(exec.Command,_eventLogger),
                    ILocoCmd cmd    => new LayoutLocoCmdUpdater().Process(exec.Command,_eventLogger),
                    ISensorCmd cmd  => new LayoutSensorCmdUpdater().Process(exec.Command,_eventLogger),
                    ISignalCmd cmd  => new LayoutSignalCmdUpdater().Process(exec.Command,_eventLogger),
                    ISystemCmd cmd  => new LayoutSystemCmdUpdater().Process(exec.Command,_eventLogger),
                    IConsistCmd cmd => new LayoutConsistCmdUpdater().Process(exec.Command,_eventLogger),
                    ICVCmd cmd      => new LayoutCvCmdUpdater().Process(exec.Command,_eventLogger),
                    _               => new LayoutGenericCmdUpdater().Process(exec.Command,_eventLogger)
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