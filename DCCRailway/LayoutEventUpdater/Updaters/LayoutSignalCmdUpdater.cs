using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.LayoutEventUpdater.Updaters;

public class LayoutSignalCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command) {

        if (command is ISignalCmd signalCmd) {
            var signals = RailwayConfig.Instance.SignalRepository;
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