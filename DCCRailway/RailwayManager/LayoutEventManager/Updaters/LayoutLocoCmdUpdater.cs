using DCCRailway.Common.Types;
using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.LayoutEventUpdater.Updaters;

public class LayoutLocoCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {

        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        if (command is ILocoCmd locoCmd) {
            var locomotives = RailwayConfig.Instance.LocomotiveRepository;
            var loco = locomotives.Find(x => x.Address == locoCmd.Address).Result;
            //var loco = Config.Locomotives[locoCmd.Address];
            if (loco is null) {
                logger.Error(locoCmd.GetType(), $"Command {command.AttributeInfo().Name} - no matching Accessory {((IAccyCmd)command).Address.Address}.");
                return false;
            }

            switch (command) {
            case ICmdLocoStop cmd:
                logger.Event(cmd.GetType(), "Setting Loco to Stop.");
                loco.LastDirection = loco.Direction;
                loco.LastSpeed     = loco.Speed;
                loco.Direction     = DCCDirection.Stop;
                loco.Speed         = new DCCSpeed(0);
                break;
            case ICmdLocoSetFunctions cmd:
                logger.Event(cmd.GetType(), "Setting Loco Functions.");
                break;
            case ICmdLocoSetFunction cmd:
                logger.Event(cmd.GetType(), "Setting Loco Function");
                break;
            case ICmdLocoSetMomentum cmd:
                logger.Event(cmd.GetType(), "Setting Loco Momentum.");
                loco.Momentum = cmd.Momentum;
                break;
            case ICmdLocoSetSpeed cmd:
                logger.Event(cmd.GetType(), "Setting Loco Speed.");
                loco.Speed     = cmd.Speed;
                loco.LastSpeed = cmd.Speed;
                break;
            case ICmdLocoSetSpeedSteps cmd:
                logger.Event(cmd.GetType(), "Setting Loco Speed Steps.");
                break;
            case ICmdLocoOpsProg cmd:
                logger.Event(cmd.GetType(), "Setting Loco To ops Programming.");
                break;
            default:
                logger.Error(command.GetType(),$"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        else {
            logger.Error(command.GetType(), $"Command {command.AttributeInfo().Name} not supported.");
        }
        return true;
    }
}