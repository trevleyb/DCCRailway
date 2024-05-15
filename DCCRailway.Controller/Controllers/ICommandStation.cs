using DCCRailway.Common.Parameters;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Tasks;

namespace DCCRailway.Controller.Controllers;

public interface ICommandStation : IParameterMappable {
    List<CommandAttribute> Commands { get; }

    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    IAdapter?              Adapter  { get; set; }
    List<AdapterAttribute> Adapters { get; }

    // Attach or detect background tasks on the Command Station
    // ----------------------------------------------------------------------------
    List<TaskAttribute>                            Tasks { get; }
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    void Start();
    void Stop();

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    TCommand?        CreateCommand<TCommand>() where TCommand : ICommand;
    TCommand?        CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand;
    IAdapter?        CreateAdapter(string? name);
    IControllerTask? CreateTask(string taskType);
    IControllerTask? AttachTask(IControllerTask task);
    IControllerTask? AttachTask(string name, string taskType, TimeSpan? frequency);
    IControllerTask? AttachTask(string name, IControllerTask task, TimeSpan? frequency);
    void             StartAllTasks();
    void             StopAllTasks();

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    DCCAddress CreateAddress();
    DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Helpers to ensure that we can check if Actions and Adapters are supported.
    // ---------------------------------------------------------------------------
    bool IsCommandSupported<T>() where T : ICommand;
    bool IsCommandSupported(Type command);
    bool IsCommandSupported(string name);

    bool IsAdapterSupported<T>() where T : IAdapter;
    bool IsAdapterSupported(Type adapter);
    bool IsAdapterSupported(string name);

    void OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result);
}