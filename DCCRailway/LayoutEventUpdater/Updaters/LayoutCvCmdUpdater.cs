using DCCRailway.Common.Utilities;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.LayoutEventUpdater.Updaters;

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