using DCCRailway.Configuration;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutConsistCmdUpdaterUpdater (DCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
    public new bool Process(ICommand command) {
  
        switch (command) {
        case ICmdConsistCreate cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistKill cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistDelete cmd:
            // TODO: Implement the command processing
            break;
        case ICmdConsistAdd cmd:
            // TODO: Implement the command processing
            break;
        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }
        return true;
    }
}