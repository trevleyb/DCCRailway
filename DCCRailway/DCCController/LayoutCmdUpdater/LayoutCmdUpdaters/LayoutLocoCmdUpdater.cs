using DCCRailway.Common.Utilities;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.BaseTypes;
using DCCRailway.Layout;

namespace DCCRailway.DCCController.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutLocoCmdUpdater(DCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
    public new bool Process(ICommand command) {
        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        var loco = Config.Locomotives[((ILocoCmd)command).Address];
        if (loco is null) {
            Logger.Log.Error($"Command {command.AttributeInfo().Name} - no matching Accessory {((IAccyCmd)command).Address.Address}.");

            return false;
        }

        switch (command) {
        case ICmdLocoStop cmd:
            break;
        case ICmdLocoSetFunctions cmd:
            break;
        case ICmdLocoSetMomentum cmd:
            break;
        case ICmdLocoSetSpeed cmd:
            break;
        case ICmdLocoSetSpeedSteps cmd:
            break;
        case ICmdLocoOpsProg cmd:
            break;
        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");

            throw new Exception("Unexpected type of command executed.");
        }

        return true;
    }
}