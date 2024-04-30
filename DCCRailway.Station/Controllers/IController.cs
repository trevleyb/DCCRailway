using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Controllers.Events;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Station.Controllers;

public interface IController : IParameterMappable {
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    public void Start();
    public void Stop();

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    public ICommandResult         Execute(ICommand command);
    public TCommand?              CreateCommand<TCommand>() where TCommand : ICommand;
    public List<CommandAttribute> Commands { get; }

    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    public IAdapter?      Adapter { get; set; }
    public IAdapter?      CreateAdapter(string? name);
    public List<AdapterAttribute> Adapters { get; }

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    public IDCCAddress CreateAddress();
    public IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Helpers to ensure that we can check if Commands and Adapters are supported.
    // ---------------------------------------------------------------------------
    public bool IsCommandSupported<T>() where T : ICommand;
    public bool IsCommandSupported(Type command);
    public bool IsCommandSupported(string name);

    public bool IsAdapterSupported<T>() where T : IAdapter;
    public bool IsAdapterSupported(Type adapter);
    public bool IsAdapterSupported(string name);

}