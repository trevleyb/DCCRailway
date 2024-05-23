using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.SystemEvents;

public class SystemEventCommandArgs : SystemEventArgs {
    public SystemEventCommandArgs(ICommand command, ICmdResult result, string message) {
        Type        = SystemEventType.Command;
        Action      = SystemEventAction.Execute;
        Message     = message;
        Name        = command?.AttributeInfo()?.Name ?? "Unknown";
        Description = command?.AttributeInfo()?.Description ?? "Unknown";

        switch (command) {
        case ILocoCmd locoCommand:
            Address = locoCommand.Address;
            Description =
                $"Executed command '{Name}' on Loco '{Address}' with a resultOld of '{result.Success}' and a value of '{result}' - {Message}";

            break;
        case IAccyCmd accyCommand:
            Address = accyCommand.Address;
            Description =
                $"Executed command '{Name}' on Accessory '{Address}' with a resultOld of '{result.Success}' and a value of '{result}' - {Message}";

            break;
        case ISensorCmd sensorCommand:
            Address = new DCCAddress(0);
            Description =
                $"Executed command '{Name}' on Sensor '{Address}' with a resultOld of '{result.Success}' and a value of '{result}' - {Message}";

            break;
        case ISignalCmd signalCommand:
            Address = signalCommand.Address;
            Description =
                $"Executed command '{Name}' on Signal '{Address}' with a resultOld of '{result.Success}' and a value of '{result}' - {Message}";

            break;
        case ISystemCmd systemCommand:
            Address = new DCCAddress(0);
            Description =
                $"Executed command '{Name}' on the System with a resultOld of '{result.Success}' and a value of '{result}' - {Message}";

            break;
        default:
            Description =
                $"Executed command '{Name}' with a result of '{result.Success}' and a value of '{result}' - {{Message}}";

            break;
        }
    }

    public string     Name        { get; set; }
    public string     Description { get; set; }
    public string     Message     { get; set; }
    public DCCAddress Address     { get; set; }

    public override string ToString() {
        return Description;
    }
}