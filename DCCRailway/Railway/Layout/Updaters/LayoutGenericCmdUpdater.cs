using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.Railway.Layout.Updaters;

public class LayoutGenericCmdUpdater() {
    public bool Process(ICommand command, LayoutEventLogger logger) {
        logger.Event(command.GetType(),$"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}