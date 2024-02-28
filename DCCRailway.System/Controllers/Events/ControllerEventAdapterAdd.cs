using DCCRailway.System.Adapters;

namespace DCCRailway.System.Controllers.Events;

public class ControllerEventAdapterAdd(IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter Adapter { get; set; } = adapter;
}