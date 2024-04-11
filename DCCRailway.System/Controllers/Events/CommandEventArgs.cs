using System.Runtime.CompilerServices;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Controllers.Events;

public class CommandEventArgs(ICommand command, ICommandResult result,  string message = "") : ControllerEventArgs(command,null,result,null,message) {

}