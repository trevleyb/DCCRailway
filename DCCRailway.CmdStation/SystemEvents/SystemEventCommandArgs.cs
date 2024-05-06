using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.SystemEvents;

public class SystemEventCommandArgs : SystemEventArgs {
    public SystemEventCommandArgs(ICommand command, ICommandResult result, string message) {
        Type        = SystemEventType.Command;
        Action      = SystemEventAction.Execute;
        Message     = message;
        Name        = command.AttributeInfo().Name;
        Description = command.AttributeInfo().Description;

        switch (command) {
        case ILocoCmd locoCommand:
            Address     = locoCommand.Address;
            Description = $"Executed command '{Name}' on Loco '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";

            break;
        case IAccyCmd accyCommand:
            Address     = accyCommand.Address;
            Description = $"Executed command '{Name}' on Accessory '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";

            break;
        case ISensorCmd sensorCommand:
            Address     = new DCCAddress(0);
            Description = $"Executed command '{Name}' on Sensor '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";

            break;
        case ISignalCmd signalCommand:
            Address     = signalCommand.Address;
            Description = $"Executed command '{Name}' on Signal '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";

            break;
        case ISystemCmd systemCommand:
            Address     = new DCCAddress(0);
            Description = $"Executed command '{Name}' on the System with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";

            break;
        default:
            Description = $"Executed command '{Name}' with a result of '{result.IsOK}' and a value of '{result.ToString()}' - {{Message}}";

            break;
        }
    }

    public string      Name        { get; set; }
    public string      Description { get; set; }
    public string      Message     { get; set; }
    public IDCCAddress Address     { get; set; }

    public override string ToString() => Description;
}