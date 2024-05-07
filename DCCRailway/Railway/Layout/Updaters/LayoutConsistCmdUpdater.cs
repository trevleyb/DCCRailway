using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.Railway.Layout.Updaters;

public class LayoutConsistCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {
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