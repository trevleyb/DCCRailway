using DCCRailway.Railway.Configuration;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Railway.Layout.Updaters;

public class LayoutSensorCmdUpdater() : LayoutGenericCmdUpdater() {
    public new bool Process(ICommand command, LayoutEventLogger logger) {

        if (command is ISensorCmd sensorCmd) {
            var sensor = RailwayConfig.Instance.Sensors.Find(x => x.Address == sensorCmd.Address);
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