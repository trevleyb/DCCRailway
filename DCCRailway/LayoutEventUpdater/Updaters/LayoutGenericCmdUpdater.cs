using DCCRailway.Common.Utilities;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

namespace DCCRailway.LayoutEventUpdater.Updaters;

public class LayoutGenericCmdUpdater() {
    public bool Process(ICommand command) {
        Logger.Log.Information($"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}