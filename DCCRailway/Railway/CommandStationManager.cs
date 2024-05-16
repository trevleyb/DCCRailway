using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Railway.Layout;
using Serilog;

namespace DCCRailway.Railway;

public class CommandStationManager(ILogger logger) {
    private StateEventProcessor _processor;
    public  ICommandStation     CommandStation { get; private set; }

    public void Start(StateEventProcessor processor) {
        // Wire up the events from the Command Station so we can track Entities Property Changes
        // --------------------------------------------------------------------------------------------
        _processor                     =  processor;
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
    ///     Looks at the configuration and instantiates the commandStation for the Entities. This includes adding appropriate
    ///     adapters to the commandStation and pushing any parameters into the Controllers and Adapaters as defined in the
    ///     configuration.
    /// </summary>
    public void Configure(Configuration.Entities.Controller controller) {
        if (controller is null) throw new ApplicationException("Cannot start the Entities Layout as no Controllers are defined.");

        var commandStation = CreateCommandStationController(controller);
        if (commandStation != null) {
            AttachCommandStationAdapter(controller, commandStation);
            AttachCommandStationTasks(controller, commandStation);
            CommandStation = commandStation;
        }
    }

    /// <summary>
    ///     Create an instance of the CommandStation Controller based on the name, and attach any
    ///     additional properties to the controller as defined in the configuration.
    /// </summary>
    /// <param name="controller">Configuration Collection for the controller to create</param>
    /// <returns>An instance of a Command Station Controller or NULL if it was unable to do so. </returns>
    /// <exception cref="ControllerException">Thrown if it cannot create the controller. </exception>
    private ICommandStation? CreateCommandStationController(Configuration.Entities.Controller controller) {
        var controllerManager = new CommandStationFactory(logger);
        try {
            var commandStation = controllerManager.CreateController(controller.Name) ??
                                 throw new ControllerException($"Invalid CommandStation Name specified {controller.Name}");

            foreach (var parameter in controller.Parameters) {
                if (commandStation.IsMappableParameter(parameter.Name)) commandStation.SetMappableParameter(parameter.Name, parameter.Value);
            }
            return commandStation;
        } catch (Exception ex) {
            logger.ForContext<CommandStationManager>().Error("Unable to instantiate an instance of the specified commandStation: {0} => {1}", controller, ex.Message);
            throw;
        }
    }

    /// <summary>
    ///     Attaches an adapter to the commandStation and configures it using the provided parameters.
    /// </summary>
    /// <param name="controller">The DCCController object representing the controller for the commandStation.</param>
    /// <param name="commandStation">The ICommandStation object representing the commandStation.</param>
    /// <exception cref="AdapterException">Thrown when unable to create an Adapter.</exception>
    private void AttachCommandStationAdapter(Configuration.Entities.Controller controller, ICommandStation commandStation) {
        // Now that we have a commandStation, attach the Adapter to the commandStation and
        // configure the Adapter using the provided Parameters.
        // -----------------------------------------------------------------------------
        try {
            if (controller.Adapters.Count != 1) throw new ControllerException("Only a single Adapter can currently be configured for a Controller.");
            if (controller.Adapters[0] is { } controllerAdapter) {
                var adapterInstance = commandStation.CreateAdapter(controllerAdapter.Name) ??
                                      throw new AdapterException("Unable to create an Adapter of type: {0}", controllerAdapter.Name);

                foreach (var parameter in controllerAdapter.Parameters) {
                    if (adapterInstance.IsMappableParameter(parameter.Name)) adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                }
                commandStation.Adapter = adapterInstance;
            }
        } catch (Exception ex) {
            logger.ForContext<CommandStationManager>().Error("Unable to instantiate an instance of the specified adapter: {0} => {1}", controller?.Adapters, ex.Message);
            throw;
        }
    }

    /// <summary>
    ///     Attaches tasks from a DCCController to a ICommandStation. This method creates instances of the tasks
    ///     and sets their properties based on the configuration provided in the DCCController. The tasks are then
    ///     attached to the ICommandStation.
    /// </summary>
    /// <param name="controller">The DCCController containing the tasks to be attached.</param>
    /// <param name="commandStation">The ICommandStation to which the tasks will be attached.</param>
    private void AttachCommandStationTasks(Configuration.Entities.Controller controller, ICommandStation commandStation) {
        try {
            foreach (var task in controller.Tasks) {
                try {
                    var taskInstance = commandStation.CreateTask(task.Type);
                    if (taskInstance is not null) {
                        taskInstance.Name      = task.Name;
                        taskInstance.Frequency = task.Frequency;
                        foreach (var parameter in task.Parameters) {
                            if (taskInstance.IsMappableParameter(parameter.Name)) taskInstance.SetMappableParameter(parameter.Name, parameter.Value);
                        }
                    }
                } catch (Exception ex) {
                    logger.ForContext<CommandStationManager>().Error($"Unable to instantiate the task '{task.Name}' or type '{task.Type}'", ex);
                }
            }
        } catch (Exception ex) {
            logger.ForContext<CommandStationManager>().Error("Unable to create and attach tasks to the Command Station.", ex.Message);
            throw;
        }
    }

    private void CommandStationInstanceOnCommandStationEvent(object? sender, ControllerEventArgs e) {
        _processor.ProcessCommandEvent(e);
    }
}