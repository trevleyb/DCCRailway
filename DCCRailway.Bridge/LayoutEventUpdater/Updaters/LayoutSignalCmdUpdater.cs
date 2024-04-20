using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutSignalCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command) {

        if (command is ISignalCmd signalCmd) {
            var signals = RailwayConfig.Instance.Signals;
            var signal = signals.Find(x => x.Address == signalCmd.Address).Result;
            switch (signalCmd) {

            case ICmdSignalSetAspect cmd:
                // TODO: Implement the command processing
                break;
            default:
                Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}