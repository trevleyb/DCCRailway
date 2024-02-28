using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Adapters.Events;

namespace DCCRailway.DCCController.Controllers.Events;

public class ControllerEventAdapter(IAdapter adapter, IAdapterEvent adapterEvent, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter Adapter { get; set; } = adapter;
    public IAdapterEvent AdapterEvent { get; set; } = adapterEvent;
}