using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Application.LayoutEventManager.Updaters;

public class LayoutAccyCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {

        if (command is IAccyCmd accyCmd) {
            var accessory = RailwayConfig.Instance.Accessories.Find(x => x.Address == accyCmd.Address);

            if (accessory is null) {
                logger.Error(accyCmd.GetType(), $"Command {command.AttributeInfo().Name} - no matching Accessory {accyCmd.Address.Address}.");
                return false;
            }

            switch (accyCmd) {
            case ICmdAccyOpsProg cmd: {
                // TODO: Implement the command processing
                logger.Event(cmd.GetType(), "Accy Ops Prog");
                accessory.Parameters["opsMode"].Value = cmd.Value.ToString();
                break;
            }

            case ICmdAccySetState cmd: {
                // TODO: Implement the command processing
                logger.Event(cmd.GetType(), "Accy Set State");
                accessory.Parameters["state"].Value = cmd.State.ToString();
                break;
            }

            default:
                logger.Error(command.GetType(),$"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}