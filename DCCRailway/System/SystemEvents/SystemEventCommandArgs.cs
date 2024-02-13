using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Types;
using DCCRailway.Utilities;

namespace DCCRailway.System.SystemEvents;

public class SystemEventCommandArgs : SystemEventArgs {
    public SystemEventCommandArgs(ICommand command, ICommandResult result, string message) {
        Type    = SystemEventType.Command;
        Action  = SystemEventAction.Execute;
        Message = message;
        Name = command.Info().Name;
        Description = command.Info().Description;
        
        switch (command) {
        case ILocoCommand locoCommand:
            Address     = locoCommand.Address;
            Description = $"Executed command '{Name}' on Loco '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";
            break;
        case IAccyCommand accyCommand:
            Address     = accyCommand.Address;
            Description = $"Executed command '{Name}' on Accessory '{Address}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {Message}";
            break;
        default:
            Description = $"Executed command '{Name}' with a resultOld of '{result.IsOK}' and a value of '{result.ToString()}' - {{Message}}";
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