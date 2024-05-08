using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.Railway.Layout.Updaters;

public class LayoutSystemCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {
        switch (command) {
        case ICmdStatus cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Getting the Status of the System");
            break;
        case ICmdClockRead cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Read the Clock");
            break;
        case ICmdClockSet cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Set the Clock");
            break;
        case ICmdClockStart cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Start the Clock");
            break;
        case ICmdClockStop cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Stop the Clock");
            break;
        case ICmdMacroRun cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Run a Macro");
            break;
        case ICmdPowerGetState cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Get Current Power State");
            break;
        case ICmdPowerSetOff cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Set Power OFF");
            break;
        case ICmdPowerSetOn cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Set Power ON");
            break;
        case ICmdTrackMain cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Switch to Main Track");
            break;
        case ICmdTrackProg cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Switch to Programming Track");
            break;
        case IDummyCmd cmd:
            // TODO: Implement the command processing
            logger.Event(cmd.GetType(), "Dummy Command");
            break;

        default:
            logger.Error(command.GetType(),$"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}