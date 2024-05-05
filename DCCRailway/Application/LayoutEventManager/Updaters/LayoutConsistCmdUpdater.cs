using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.LayoutEventManager.Updaters;

public class LayoutConsistCmdUpdater() : LayoutGenericCmdUpdater() {
    public new async Task<bool> Process(ICommand command, LayoutEventLogger logger) {
        switch (command) {
        case ICmdConsistCreate cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Create a Consist");
            break;
        case ICmdConsistKill cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Kill a Consist");
            break;
        case ICmdConsistDelete cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Delete a Consist");
            break;
        case ICmdConsistAdd cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Add a Consist");
            break;
        default:
            logger.Error(command.GetType(),$"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}