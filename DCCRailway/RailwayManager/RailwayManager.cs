using System.ComponentModel.Design.Serialization;
using DCCRailway.Common.Utilities;
using DCCRailway.Layout.Configuration;
using DCCRailway.Station;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.Exceptions;
using DCCRailway.Station.Adapters.Helpers;

namespace DCCRailway.RailwayManager;

/// <summary>
/// Railway Manager loads config, starts controllers, supports restarting and shutting down
/// and acts as the meditor between the Layout and the Controllers.
/// </summary>
public class RailwayManager() {

    private List<IController> _controllers;

    public void Startup() {

        // Load up the configuration from the file
        // ToDo: Make the file a parameter on the command line or config file.
        RailwayConfig.Load();
        _controllers = new List<IController>();

        var controllerManager = new ControllerFactory();
        foreach (var controller in RailwayConfig.Instance.ControllerRepository.GetAllAsync().Result) {

            IController controllerInstance;
            try {
                controllerInstance = controllerManager.CreateController(controller.ControllerName);
                if (controllerInstance is null) throw new ControllerException($"Invalid Controller Name specified {controller}");

                foreach (var parameter in controller.Parameters) {
                    if (controllerInstance.IsMappableParameter(parameter.Name)) {
                        controllerInstance.SetMappableParameter(parameter.Name, parameter.Value);
                    }
                }

            } catch (Exception ex) {
                Logger.Log.Error("Unable to instantiate an instance of the specified controller: {0}", controller);
                throw;
            }

            // Now that we have a controller, attach the Adapter to the controller and
            // configure the Adapter using the provided Parameters.
            // -----------------------------------------------------------------------------
            IAdapter adapterInstance;
            try {
                if (controller?.Adapter?.AdapterName != null) {
                    adapterInstance = controllerInstance.CreateAdapter(controller?.Adapter?.AdapterName);
                    if (adapterInstance is null) throw new AdapterException("Unable to create an Adapter of type: {0}", controller.Adapter.AdapterName);

                    foreach (var parameter in controller.Adapter.Parameters) {
                        if (adapterInstance.IsMappableParameter(parameter.Name)) {
                            adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                        }
                    }
                    controllerInstance.Adapter = adapterInstance;
                }
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to instantiate an instance of the specified adapter: {0}", controller?.Adapter?.AdapterName);
                throw;

            }
        }


    }

    /// <summary>
    /// Restart te Railway Manager. Used if the configuration has changed.
    /// </summary>
    public void Restart() {
        Shutdown();
        Startup();
    }

    /// <summary>
    /// Shutdown the Railway System cleanly and ensure configuration is saved.
    /// </summary>
    public void Shutdown() {
        foreach (var controller in _controllers) {
            controller.Adapter?.Disconnect();
        }
        _controllers = new List<IController>();

        // Save the Configuration
        RailwayConfig.Instance.Save();
    }

}