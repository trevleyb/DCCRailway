using DCCRailway.CmdStation.Controllers;
using DCCRailway.CmdStation.Controllers.Events;
using DCCRailway.CmdStation.Exceptions;
using DCCRailway.CmdStation.Helpers;
using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;

namespace DCCRailway.Railway.CmdStation;

public class CmdStationManager {

    public IController    Controller;
    private LayoutUpdater _layoutUpdater;

    /// <summary>
    /// Looks at the configuration and instantiates the controller for the Entities. This includes adding appropriate
    /// adapters to the controller and pushing any parameters into the Controllers and Adapaters as defined in the
    /// configuration.
    /// </summary>
    public void Start(IRailwayConfig config, LayoutUpdater layoutUpdater) {

        _layoutUpdater = layoutUpdater;

        // While we can store multiple contollers in the Config, only one of them
        // can be active at any time and is designated by the default flag or the name
        // provided in the parameter.
        // ---------------------------------------------------------------------------------
        var controller = config.Controller ?? throw new ApplicationException("Cannot start the Entities Layout as no Controllers are defined.");

        // Insantiate the Controller and return it
        // ---------------------------------------------------------------------------------
        var controllerManager = new ControllerFactory();
        try {
            Controller = controllerManager.CreateController(controller.Name) ??
                throw new ControllerException($"Invalid Controller Name specified {controller.Name}");

            foreach (var parameter in controller.Parameters) {
                if (Controller.IsMappableParameter(parameter.Name)) {
                    Controller.SetMappableParameter(parameter.Name, parameter.Value);
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
                var adapterInstance = Controller.CreateAdapter(controllerAdapter.AdapterName) ??
                                      throw new AdapterException("Unable to create an Adapter of type: {0}", controllerAdapter.AdapterName);

                foreach (var parameter in controllerAdapter.Parameters) {
                    if (adapterInstance.IsMappableParameter(parameter.Name)) {
                        adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                    }
                }
                Controller.Adapter = adapterInstance;
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified adapter: {0} => {1}", controller?.Adapter?.AdapterName, ex.Message);
            throw;
        }

        // Wire up the events from the Command Station so we can track Entities Property Changes
        // --------------------------------------------------------------------------------------------
        Controller.ControllerEvent += ControllerInstanceOnControllerEvent;
        Controller.Start();
    }

    public void Stop() {
        Controller.ControllerEvent -= ControllerInstanceOnControllerEvent;
        Controller.Stop();
    }

    private void ControllerInstanceOnControllerEvent(object? sender, ControllerEventArgs e) {
        _layoutUpdater.ProcessCommandEvent(e);
    }
}