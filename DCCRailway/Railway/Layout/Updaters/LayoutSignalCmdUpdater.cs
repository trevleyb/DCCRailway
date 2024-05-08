using DCCRailway.Railway.Configuration;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Railway.Layout.Updaters;

public class LayoutSignalCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {

        if (command is ISignalCmd signalCmd) {
            var signal = RailwayConfig.Instance.Signals.Find(x => x.Address == signalCmd.Address);

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