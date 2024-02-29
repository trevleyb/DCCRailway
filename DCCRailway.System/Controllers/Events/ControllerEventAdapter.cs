using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;

namespace DCCRailway.System.Controllers.Events;

public class ControllerEventAdapter(IAdapter adapter, IAdapterEvent adapterEvent, string message = "") : ControllerEventArgs(message), IControllerEventArgs {
    public IAdapter      Adapter      { get; set; } = adapter;
    public IAdapterEvent AdapterEvent { get; set; } = adapterEvent;
}