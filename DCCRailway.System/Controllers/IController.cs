using DCCRailway.Common.Types;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Controllers.Events;

namespace DCCRailway.System.Controllers;

public interface IController {
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    public ICommandResult         Execute(ICommand command);
    public TCommand?              CreateCommand<TCommand>() where TCommand : ICommand;
    public List<CommandAttribute> Commands { get; }

    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    public IAdapter?      Adapter { get; set; }
    public IAdapter?      CreateAdapter(string name);
    public List<AdapterAttribute> Adapters { get; }

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    public IDCCAddress CreateAddress();
    public IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);
}