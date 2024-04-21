using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutAccyCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command) {

        if (command is IAccyCmd accyCmd) {
            var accessories = RailwayConfig.Instance.AccessoryRepository;
            var accessory = accessories.Find(x => x.Address == accyCmd.Address).Result;
            if (accessory is null) {
                Logger.Log.Error($"Command {command.AttributeInfo().Name} - no matching Accessory {accyCmd.Address.Address}.");
                return false;
            }

            switch (accyCmd) {
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
        }
        return true;
    }
}