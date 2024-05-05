using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

namespace DCCRailway.Application.LayoutEventManager.Updaters;

public class LayoutGenericCmdUpdater() {
    public async Task<bool> Process(ICommand command, LayoutEventLogger logger) {
        logger.Event(command.GetType(),$"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}