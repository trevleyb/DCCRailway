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
    List<TaskAttribute> Tasks { get; }

    void Start();
    void Stop();

    void                                           OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result);
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    TCommand? CreateCommand<TCommand>() where TCommand : ICommand;
    TCommand? CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand;

    IAdapter? CreateAdapter(string? name);

    IControllerTask? CreateTask(string taskType);
    IControllerTask? CreateTask(string name, string taskType, TimeSpan? frequency);
    IControllerTask? CreateTask(string name, IControllerTask task, TimeSpan? frequency);
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

    // Helper to manage Function Blocks. As Functions are not sent to a Command Station as a function
    // number and state but as a block of functions, we need to track what the current state is of each 
    // "thing" we are addressing. This is done by the FunctionBlocks class.
    // ------------------------------------------------------------------------------------------------
    DCCFunctionBlocks FunctionBlocks(DCCAddress address);
    DCCFunctionBlocks FunctionBlocks(int address);
}