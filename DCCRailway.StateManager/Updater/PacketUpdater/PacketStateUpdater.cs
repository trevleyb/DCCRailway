using DCCRailway.Common.Helpers;
using DCCRailway.StateManager.State;
using Serilog;

namespace DCCRailway.StateManager.Updater.PacketUpdater;

/// <summary>
///     The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
///     and the Entities Configuration itself.This is because, while the layout might issue a command
///     to the command statin and it will be executed, other systems might issue a command also
///     and so if these are detected (for example if we have a DC packet analyser listening to commands)
///     then we need to update the Entities data with these changes.
///     So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
///     the collection of all data related to the current executing layout.
/// </summary>
public class PacketStateUpdater(ILogger logger, IStateManager stateManager) : IStateUpdater {
    public IResult Process(object message) {
        return Result.Ok();
    }
}