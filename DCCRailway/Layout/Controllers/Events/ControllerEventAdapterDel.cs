using DCCRailway.Layout.Adapters;

namespace DCCRailway.Layout.Controllers.Events;

public class ControllerEventAdapterDel(IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter Adapter { get; set; } = adapter;
}