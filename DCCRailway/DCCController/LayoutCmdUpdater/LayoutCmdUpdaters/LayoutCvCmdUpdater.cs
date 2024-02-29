using DCCRailway.Common.Utilities;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.Layout;

namespace DCCRailway.DCCController.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutCvCmdUpdater(DCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
    public new bool Process(ICommand command) {
        switch (command) {
        case ICmdCVRead cmd:
            // TODO: Implement the command processing
            break;
        case ICmdCVWrite cmd:
            // TODO: Implement the command processing
            break;
        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");

            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}