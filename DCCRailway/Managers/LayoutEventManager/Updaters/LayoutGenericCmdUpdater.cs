using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

namespace DCCRailway.Managers.LayoutEventManager.Updaters;

public class LayoutGenericCmdUpdater() {
    public bool Process(ICommand command, LayoutEventLogger logger) {
        logger.Event(command.GetType(),$"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}