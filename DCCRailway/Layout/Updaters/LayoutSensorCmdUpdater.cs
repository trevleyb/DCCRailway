using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Configuration;

namespace DCCRailway.Layout.Updaters;

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