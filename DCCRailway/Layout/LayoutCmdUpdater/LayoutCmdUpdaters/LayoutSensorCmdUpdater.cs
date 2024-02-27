using DCCRailway.Configuration;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.LayoutCmdUpdater.LayoutCmdUpdaters;
public class LayoutSensorCmdUpdater(DCCRailwayConfig config) : LayoutGenericCmdUpdater(config) {
    public new bool Process(ICommand command) {

        // Get the Accessory from the configuration so that we can update its state
        // -----------------------------------------------------------------------------
        //var Sensor = config.Sensors[((ISensorCmd)command). .Address];
        //if (loco is null) {
        //    Logger.Log.Error($"Command {command.AttributeInfo().Name} - no matching Accessory {((IAccyCmd)command).Address.Address}.");
        //    return false;
        //}
        
        switch (command) {
        case ICmdSensorGetState cmd:
            // TODO: Implement the command processing
            break;
        default:
            Logger.Log.Error($"Command {command.AttributeInfo().Name} not supported.");
            throw new Exception("Unexpected type of command executed.");
        }
        return true;
    }
}