using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutLocoCmdUpdater(IDCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
    public new bool Process(ICommand command) {
        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        if (command is ILocoCmd locoCmd) {
            var loco = Config.Locomotives[locoCmd.Address];
            if (loco is null) {
                Logger.Log.Error(
                    $"Command {command.AttributeInfo().Name} - no matching Accessory {((IAccyCmd)command).Address.Address}.");
                return false;
            }

            switch (command) {
            case ICmdLocoStop cmd:
                loco.LastDirection = loco.Direction;
                loco.LastSpeed     = loco.Speed;
                loco.Direction     = DCCDirection.Stop;
                loco.Speed         = new DCCSpeed(0);
                break;
            case ICmdLocoSetFunctions cmd:
                break;
            case ICmdLocoSetFunction cmd:
                break;
            case ICmdLocoSetMomentum cmd:
                loco.Momentum = cmd.Momentum;
                break;
            case ICmdLocoSetSpeed cmd:
                loco.Speed     = cmd.Speed;
                loco.LastSpeed = cmd.Speed;
                break;
            case ICmdLocoSetSpeedSteps cmd:
                break;
            case ICmdLocoOpsProg cmd:
                break;
            default:
                Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        else {
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
        }
        return true;
    }
}