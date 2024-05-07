using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.Railway.Layout.Updaters;

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