using DCCRailway.Common.Types;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Controllers.Events;

namespace DCCRailway.System.Controllers;

public interface IController {
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    public List<(Type Command, string Name)> Commands { get; }
    public List<(Type Adapter, string Name)> Adapters { get; }

    public bool IsCommandSupported<T>() where T : ICommand;
    public bool IsAdapterSupported<T>() where T : IAdapter;
    public bool IsCommandSupported(string name);
    public bool IsAdapterSupported(string name);

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    public ICommandResult Execute(ICommand command);
    public TCommand?      CreateCommand<TCommand>() where TCommand : ICommand;

    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    public IAdapter? Adapter { get; set; }
    public IAdapter? CreateAdapter(string name);

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    public IDCCAddress CreateAddress();
    public IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);
}