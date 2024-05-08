using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Helpers;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;

namespace DCCRailway.Railway;

public class CommandStationManager {

    public ICommandStation  CommandStation;
    private LayoutUpdater   _layoutUpdater;

    /// <summary>
    /// Looks at the configuration and instantiates the commandStation for the Entities. This includes adding appropriate
    /// adapters to the commandStation and pushing any parameters into the Controllers and Adapaters as defined in the
    /// configuration.
    /// </summary>
    public void Start(IRailwayConfig config, LayoutUpdater layoutUpdater) {

        _layoutUpdater = layoutUpdater;

        // While we can store multiple contollers in the Config, only one of them
        // can be active at any time and is designated by the default flag or the name
        // provided in the parameter.
        // ---------------------------------------------------------------------------------
        var controller = config.CommandStation ?? throw new ApplicationException("Cannot start the Entities Layout as no Controllers are defined.");

        // Insantiate the CommandStation and return it
        // ---------------------------------------------------------------------------------
        var controllerManager = new CommandStationFactory();
        try {
            CommandStation = controllerManager.CreateController(controller.Name) ??
                throw new ControllerException($"Invalid CommandStation Name specified {controller.Name}");

            foreach (var parameter in controller.Parameters) {
                if (CommandStation.IsMappableParameter(parameter.Name)) {
                    CommandStation.SetMappableParameter(parameter.Name, parameter.Value);
                }
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified commandStation: {0} => {1}", controller, ex.Message);
            throw;
        }

        // Now that we have a commandStation, attach the Adapter to the commandStation and
        // configure the Adapter using the provided Parameters.
        // -----------------------------------------------------------------------------
        try {
            if (controller?.Adapter is { } controllerAdapter) {
                var adapterInstance = CommandStation.CreateAdapter(controllerAdapter.AdapterName) ??
                                      throw new AdapterException("Unable to create an Adapter of type: {0}", controllerAdapter.AdapterName);

                foreach (var parameter in controllerAdapter.Parameters) {
                    if (adapterInstance.IsMappableParameter(parameter.Name)) {
                        adapterInstance.SetMappableParameter(parameter.Name, parameter.Value);
                    }
                }
                CommandStation.Adapter = adapterInstance;
            }
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to instantiate an instance of the specified adapter: {0} => {1}", controller?.Adapter?.AdapterName, ex.Message);
            throw;
        }

        // Wire up the events from the Command Station so we can track Entities Property Changes
        // --------------------------------------------------------------------------------------------
        CommandStation.ControllerEvent += CommandStationInstanceOnCommandStationEvent;
        CommandStation.Start();
    }

    public void Stop() {
        CommandStation.ControllerEvent -= CommandStationInstanceOnCommandStationEvent;
        CommandStation.Stop();
    }

    private void CommandStationInstanceOnCommandStationEvent(object? sender, ControllerEventArgs e) {
        _layoutUpdater.ProcessCommandEvent(e);
    }
}