using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.SystemEvents;

public class SystemEventCommandArgs : SystemEventArgs {
    public SystemEventCommandArgs(ICommand command, ICommandResult result, string message) {
        Type    = SystemEventType.Command;
        Action  = SystemEventAction.Execute;
        Message = message;
        Name = command.AttributeInfo().Name;
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
            Address = new DCCAddress(0);
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
    
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Message     { get; set; }
    public IDCCAddress Address { get; set; }

    public override string ToString() {
        return Description;
    }
}