using DCCRailway.Common.Utilities;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.LayoutEventUpdater.Updaters;

public class LayoutSystemCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command) {
        switch (command) {
        case ICmdStatus cmd:
            // TODO: Implement the command processing
            break;
        case ICmdClockRead cmd:
            // TODO: Implement the command processing
            break;
        case ICmdClockSet cmd:
            // TODO: Implement the command processing
            break;
        case ICmdClockStart cmd:
            // TODO: Implement the command processing
            break;
        case ICmdClockStop cmd:
            // TODO: Implement the command processing
            break;
        case ICmdMacroRun cmd:
            // TODO: Implement the command processing
            break;
        case ICmdPowerGetState cmd:
            // TODO: Implement the command processing
            break;
        case ICmdPowerSetOff cmd:
            // TODO: Implement the command processing
            break;
        case ICmdPowerSetOn cmd:
            // TODO: Implement the command processing
            break;
        case ICmdTrackMain cmd:
            // TODO: Implement the command processing
            break;
        case ICmdTrackProg cmd:
            // TODO: Implement the command processing
            break;
        case IDummyCmd cmd:
            // TODO: Implement the command processing
            break;

        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");

            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}