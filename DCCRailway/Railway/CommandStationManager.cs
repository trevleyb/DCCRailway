using System.Windows.Input;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Helpers;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Throttles.WiThrottle;

namespace DCCRailway.Railway;

public class CommandStationManager {

    public ICommandStation CommandStation { get; private set; }
    private StateEventProcessor _processor;

    public void Start(StateEventProcessor processor) {
        // Wire up the events from the Command Station so we can track Entities Property Changes
        // --------------------------------------------------------------------------------------------
        _processor = processor;
        CommandStation.ControllerEvent += CommandStationInstanceOnCommandStationEvent;
        CommandStation.Start();
        CommandStation.StartAllTasks();
    }

    public void Stop() {
        CommandStation.ControllerEvent -= CommandStationInstanceOnCommandStationEvent;
        CommandStation.StopAllTasks();
        CommandStation.Stop();
    }

    /// <summary>
    /// Looks at the configuration and instantiates the commandStation for the Entities. This includes adding appropriate
    /// adapters to the commandStation and pushing any parameters into the Controllers and Adapaters as defined in the
    /// configuration.
    /// </summary>
    public void Configure(DCCController dccController) {
        if (dccController is null) throw new ApplicationException("Cannot start the Entities Layout as no Controllers are defined.");

        var commandStation = CreateCommandStationController(dccController);
        if (commandStation != null) {
            AttachCommandStationAdapter(dccController, commandStation);
            AttachCommandStationTasks(dccController, commandStation);
            CommandStation = commandStation;
        }
    }

    /// <summary>
    /// Create an instance of the CommandStation Controller based on the name, and attach any
    /// additional properties to the controller as defined in the configuration.
    /// </summary>
    /// <param name="dccController">Cionfiguration Collection for the controller to create</param>
    /// <returns>An instance of a Command Station Controller or NULL if it was unable to do so. </returns>
    /// <exception cref="ControllerException">Thrown if it cannot create the controller. </exception>
    private ICommandStation? CreateCommandStationController(DCCController dccController) {
        var controllerManager = new CommandStationFactory();
        try {
            var commandStation = controllerManager.CreateController(dccController.Name) ??
                             throw new ControllerException($"Invalid CommandStation Name specified {dccController.Name}");

            foreach (var parameter in dccController.Parameters) {
                if (commandStation.IsMappableParameter(parameter.Name)) {
                    commandStation.SetMappableParameter(parameter.Name, parameter.Value);
                }
            }
            return commandStation;
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified commandStation: {0} => {1}", dccController, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Attaches an adapter to the commandStation and configures it using the provided parameters.
    /// </summary>
    /// <param name="dccController">The DCCController object representing the controller for the commandStation.</param>
    /// <param name="commandStation">The ICommandStation object representing the commandStation.</param>
    /// <exception cref="AdapterException">Thrown when unable to create an Adapter.</exception>
    private void AttachCommandStationAdapter(DCCController dccController, ICommandStation commandStation) {
        // Now that we have a commandStation, attach the Adapter to the commandStation and
        // configure the Adapter using the provided Parameters.
        // -----------------------------------------------------------------------------
        try {
            if (dccController?.Adapter is { } controllerAdapter) {
                var adapterInstance = commandStation.CreateAdapter(controllerAdapter.Name) ??
                                      throw new AdapterException("Unable to create an Adapter of type: {0}", controllerAdapter.Name);

                foreach (var parameter in controllerAdapter.Parameters) {
                    if (adapterInstance.IsMappableParameter(parameter.Name)) {
                        adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                    }
                }
                commandStation.Adapter = adapterInstance;
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified adapter: {0} => {1}", dccController?.Adapter?.Name, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Attaches tasks from a DCCController to a ICommandStation. This method creates instances of the tasks
    /// and sets their properties based on the configuration provided in the DCCController. The tasks are then
    /// attached to the ICommandStation.
    /// </summary>
    /// <param name="dccController">The DCCController containing the tasks to be attached.</param>
    /// <param name="commandStation">The ICommandStation to which the tasks will be attached.</param>
    private void AttachCommandStationTasks(DCCController dccController, ICommandStation commandStation) {
        try {
            foreach (var task in dccController.Tasks) {
                try {
                    var taskInstance = commandStation.CreateTask(task.Type);
                    if (taskInstance is not null) {
                        taskInstance.Name = task.Name;
                        taskInstance.Frequency = task.Frequency;
                        commandStation.AttachTask(taskInstance);

                        foreach (var parameter in task.Parameters) {
                            if (taskInstance.IsMappableParameter(parameter.Name)) {
                                taskInstance.SetMappableParameter(parameter.Name, parameter.Value);
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Logger.Log.Error($"Unable to instantiate the task '{task.Name}' or type '{task.Type}'");
                }
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to create and attach tasks to the Command Station.", ex.Message);
            throw;
        }
    }

    private void CommandStationInstanceOnCommandStationEvent(object? sender, ControllerEventArgs e) {
        _processor.ProcessCommandEvent(e);
    }
}