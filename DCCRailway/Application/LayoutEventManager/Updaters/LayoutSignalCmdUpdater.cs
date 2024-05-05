using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Application.LayoutEventManager.Updaters;

public class LayoutSignalCmdUpdater() : LayoutGenericCmdUpdater() {
    public new async Task<bool> Process(ICommand command, LayoutEventLogger logger) {

        if (command is ISignalCmd signalCmd) {
            var signals = RailwayConfig.Instance.Signals;
            var signal = signals.Find(x => x.Address == signalCmd.Address).Result;

            switch (signalCmd) {
            case ICmdSignalSetAspect cmd:
                // TODO: Implement the command processing
                logger.Event(cmd.GetType(), "Setting Signal Aspect.");
                break;
            default:
                logger.Error(command.GetType(), $"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}