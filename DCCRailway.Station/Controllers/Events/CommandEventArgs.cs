using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Controllers.Events;

public class CommandEventArgs(ICommand command, ICommandResult? result,  string? message = "") : ControllerEventArgs(command,null,result,null,message) {

}