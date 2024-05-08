using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Results;

namespace DCCRailway.CmdStation.Controllers.Events;

public class CommandEventArgs(ICommand command, ICmdResult? result,  string? message = "") : ControllerEventArgs(command,null,result,null,message) {

}