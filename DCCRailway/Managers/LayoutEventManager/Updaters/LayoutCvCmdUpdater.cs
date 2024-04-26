using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Managers.LayoutEventManager.Updaters;

public class LayoutCvCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {
        switch (command) {
        case ICmdCVRead cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Read a CV");
            break;
        case ICmdCVWrite cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Write a CV");
            break;
        default:
            logger.Error(command.GetType(), $"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}