using DCCRailway.Application.LayoutEventManager;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.Controllers.Events;
using DCCRailway.Station.Exceptions;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Application;

/// <summary>
/// Railway Manager loads config, starts controllers, supports restarting and shutting down
/// and acts as the meditor between the Layout and the Controllers.
/// </summary>
public class RailwayManager(IRailwayConfig? config = null) {
    private LayoutUpdater _layoutUpdater;
    private IController?  _activeController;

    public void Startup() {
        // ToDo: Make the file a parameter on the command line or config file.
        _layoutUpdater = new LayoutEventManager.LayoutUpdater();
        _activeController = InstantiateController();
    }

    public void Restart() {
        Shutdown();
        Startup();
    }

    public void Shutdown() {
        if (_activeController is not null) {
            _activeController.Stop();
            _activeController.Adapter?.Disconnect();
            _activeController.ControllerEvent -= ControllerInstanceOnControllerEvent;
            _activeController = null;
        }

        //_config.Save();
    }

    public IRailwayConfig Config { get; } = config ?? RailwayConfig.Load();
    public IController? ActiveController => _activeController;

    /// <summary>
    /// Looks at the configuration and instantiates the controller for the Layout. This includes adding appropriate
    /// adapters to the controller and pushing any parameters into the Controllers and Adapaters as defined in the
    /// configuration.
    /// </summary>
    private IController InstantiateController(string? controllerID = null) {

        Layout.Configuration.Entities.System.Controller? controller;

        // While we can store multiple contollers in the Config, only one of them
        // can be active at any time and is designated by the default flag or the name
        // provided in the parameter.
        // ---------------------------------------------------------------------------------
        try {
            controller = (controllerID is null) ? Config.Controllers.DefaultController : Config.Controllers[controllerID];
            if (controller is null) throw new ApplicationException("Cannot start the Layout Manager as no Controllers are defined.");
        }
        catch (Exception ex) {
            throw new ApplicationException("Cannot start the Layout Manager as no Controllers are defined.",ex);
        }

        // Insantiate the Controller and return it
        // ---------------------------------------------------------------------------------
        var controllerManager = new ControllerFactory();
        IController controllerInstance;
        try {
            controllerInstance = controllerManager.CreateController(controller.Name);
            if (controllerInstance is null) throw new ControllerException($"Invalid Controller Name specified {controller.Name}");

            foreach (var parameter in controller.Parameters) {
                if (controllerInstance.IsMappableParameter(parameter.Name)) {
                    controllerInstance.SetMappableParameter(parameter.Name, parameter.Value);
                }
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified controller: {0} => {1}", controller, ex.Message);
            throw;
        }

        // Now that we have a controller, attach the Adapter to the controller and
        // configure the Adapter using the provided Parameters.
        // -----------------------------------------------------------------------------
        try {
            if (controller?.Adapter is { } controllerAdapter) {
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
        return controllerInstance;
    }

    private void ControllerInstanceOnControllerEvent(object? sender, ControllerEventArgs e) {
        _layoutUpdater.ProcessCommandEvent(e);
    }
}