using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutCvCmdUpdater() : LayoutGenericCmdUpdater() {
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