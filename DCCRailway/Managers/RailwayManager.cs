using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.Controllers.Events;
using DCCRailway.Station.Exceptions;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Managers;

/// <summary>
/// Railway Manager loads config, starts controllers, supports restarting and shutting down
/// and acts as the meditor between the Layout and the Controllers.
/// </summary>
public class RailwayManager(IRailwayConfig? config = null) {

    private readonly IRailwayConfig _config = config ?? RailwayConfig.Load();
    private LayoutEventManager.LayoutEventManager _layoutEventManager;
    private List<IController> _activeControllers;

    public void Startup() {
        // ToDo: Make the file a parameter on the command line or config file.
        _layoutEventManager = new LayoutEventManager.LayoutEventManager();
        _activeControllers  = InstantiateControllers();
    }

    public void Restart() {
        Shutdown();
        Startup();
    }

    public void Shutdown() {
        foreach (var controller in _activeControllers) {
            controller.Stop();
            if (controller.Adapter is not null) controller.Adapter.Disconnect();
            controller.ControllerEvent -= ControllerInstanceOnControllerEvent;
        }
        _activeControllers = [];
        //_config.Save();
    }

    /// <summary>
    /// Looks at the configuration and instantiates the controllers for the Layout. This includes adding appropriate
    /// adapters to the controller and pushing any parameters into the Controllers and Adapaters as defined in the
    /// configuration.
    /// </summary>
    /// <param name="eventManager">An instance the the layout event manager that will manage events between command stations and configuration</param>
    /// <returns>A list of instantiated controllers</returns>
    /// <exception cref="ControllerException">Throw exception on an error instantiating a controller </exception>
    /// <exception cref="AdapterException"></exception>
    private List<IController> InstantiateControllers() {
        var controllers = new List<IController>();
        var controllerManager = new ControllerFactory();
        foreach (var controller in _config.ControllerRepository.GetAllAsync().Result) {

            IController controllerInstance;
            try {
                controllerInstance = controllerManager.CreateController(controller.Name);
                if (controllerInstance is null) throw new ControllerException($"Invalid Controller Name specified {controller}");

                foreach (var parameter in controller.Parameters) {
                    if (controllerInstance.IsMappableParameter(parameter.Name)) {
                        controllerInstance.SetMappableParameter(parameter.Name, parameter.Value);
                    }
                }

            } catch (Exception ex) {
                Logger.Log.Error("Unable to instantiate an instance of the specified controller: {0} => {1}", controller, ex.Message);
                throw;
            }

            // Now that we have a controller, attach the Adapter to the controller and
            // configure the Adapter using the provided Parameters.
            // -----------------------------------------------------------------------------
            try {
                if (controller?.Adapter is {} controllerAdapter) {
                    var adapterInstance = controllerInstance.CreateAdapter(controllerAdapter.AdapterName);
                    if (adapterInstance is null) throw new AdapterException("Unable to create an Adapter of type: {0}", controllerAdapter.AdapterName);

                    foreach (var parameter in controllerAdapter.Parameters) {
                        if (adapterInstance.IsMappableParameter(parameter.Name)) {
                            adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                        }
                    }
                    controllerInstance.Adapter = adapterInstance;
                }
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to instantiate an instance of the specified adapter: {0} => {1}", controller?.Adapter?.AdapterName, ex.Message);
                throw;
            }

            // Wire up the events from the Command Station so we can track Layout Property Changes
            // --------------------------------------------------------------------------------------------
            controllerInstance.ControllerEvent += ControllerInstanceOnControllerEvent;

            controllerInstance.Start();
            controllers.Add(controllerInstance);
        }
        return controllers;
    }

    private void ControllerInstanceOnControllerEvent(object? sender, ControllerEventArgs e) {
        _layoutEventManager.ProcessCommandEvent(e);
    }
}