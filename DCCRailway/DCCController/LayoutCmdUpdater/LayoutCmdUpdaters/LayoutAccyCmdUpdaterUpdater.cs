using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCLayout;
using DCCRailway.Utilities;

namespace DCCRailway.DCCController.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutAccyCmdUpdaterUpdater(DCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {

    public new bool Process(ICommand command) {

        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        var accessory = Config.Accessories[((IAccyCmd)command).Address];
        if (accessory is null) {
            Logger.Log.Error($"Command {command.AttributeInfo().Name} - no matching Accessory {((IAccyCmd)command).Address.Address}.");
            return false;
        }
        
        switch (command) {
        case ICmdAccyOpsProg cmd: {
            // TODO: Implement the command processing
            accessory.Parameters["opsMode"].Value = cmd.Value.ToString();
            break;
        }

        case ICmdAccySetState cmd: {
            // TODO: Implement the command processing
            accessory.Parameters["state"].Value = cmd.State.ToString();
            break;
        }

        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}
