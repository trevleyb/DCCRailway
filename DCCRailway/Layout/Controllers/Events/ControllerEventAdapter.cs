using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Adapters.Events;

namespace DCCRailway.Layout.Controllers.Events;

public class ControllerEventAdapter(IAdapter adapter, IAdapterEvent adapterEvent, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter Adapter { get; set; } = adapter;
    public IAdapterEvent AdapterEvent { get; set; } = adapterEvent;
}