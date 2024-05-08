using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Railway.Configuration;

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