using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;

namespace DCCRailway.Layout.Updaters;

public class LayoutGenericCmdUpdater() {
    public bool Process(ICommand command, LayoutEventLogger logger) {
        logger.Event(command.GetType(),$"Command {command.AttributeInfo().Name} - no matching condition. Ignored.");
        return true;
    }
}