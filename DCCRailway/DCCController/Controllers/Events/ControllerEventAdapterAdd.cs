using DCCRailway.DCCController.Adapters;

namespace DCCRailway.DCCController.Controllers.Events;

public class ControllerEventAdapterAdd(IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter Adapter { get; set; } = adapter;
}