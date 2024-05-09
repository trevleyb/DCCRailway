using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Helpers;

namespace DCCRailway.Controller.Controllers;

public interface ICommandStation : IParameterMappable {
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    public void Start();
    public void Stop();

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    public TCommand?              CreateCommand<TCommand>() where TCommand : ICommand;
    public TCommand?              CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand;
    public List<CommandAttribute> Commands { get; }

    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    public IAdapter?      Adapter { get; set; }
    public IAdapter?      CreateAdapter(string? name);
    public List<AdapterAttribute> Adapters { get; }

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    public DCCAddress CreateAddress();
    public DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Helpers to ensure that we can check if Actions and Adapters are supported.
    // ---------------------------------------------------------------------------
    public bool IsCommandSupported<T>() where T : ICommand;
    public bool IsCommandSupported(Type command);
    public bool IsCommandSupported(string name);

    public bool IsAdapterSupported<T>() where T : IAdapter;
    public bool IsAdapterSupported(Type adapter);
    public bool IsAdapterSupported(string name);

    public void OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result);

}