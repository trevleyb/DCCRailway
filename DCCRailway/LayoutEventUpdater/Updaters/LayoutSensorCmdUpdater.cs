using DCCRailway.Common.Utilities;
using DCCRailway.Layout;
using DCCRailway.Layout.Configuration;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.LayoutCmdUpdater.LayoutCmdUpdaters;

public class LayoutSensorCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command) {

        if (command is ISensorCmd sensorCmd) {
            var sensors = RailwayConfig.Instance.SensorRepository;
            var sensor = sensors.Find(x => x.Address == sensorCmd.Address).Result;
            switch (sensorCmd) {
            case ICmdSensorGetState cmd:
                break;
            default:
                Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}