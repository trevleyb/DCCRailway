using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutSystemCmdUpdater(IDCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
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