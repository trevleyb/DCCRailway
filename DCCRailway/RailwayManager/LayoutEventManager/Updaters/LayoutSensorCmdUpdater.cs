using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.LayoutEventUpdater.Updaters;

public class LayoutSensorCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {

        if (command is ISensorCmd sensorCmd) {
            var sensors = RailwayConfig.Instance.SensorRepository;
            var sensor = sensors.Find(x => x.Address == sensorCmd.Address).Result;
            switch (sensorCmd) {
            case ICmdSensorGetState cmd:
                logger.Event(cmd.GetType(), "Getting the State of a Sensor");
                break;
            default:
                logger.Error(command.GetType(),$"Command {command.AttributeInfo().Name} not supported.");
                throw new Exception("Unexpected type of command executed.");
            }
        }
        return true;
    }
}