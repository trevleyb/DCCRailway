using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.SystemEvents;

public class SystemEventAdapterArgs : SystemEventArgs {
    public SystemEventAdapterArgs(IAdapter adapter, SystemEventAction action, string message) {
        Type        = SystemEventType.Adapter;
        Action      = action;
        Message     = message;
        Name        = adapter.AttributeInfo().Name;
        Description = adapter.AttributeInfo().Description;
    }

    public string  Message     { get; set; }
    public string? Name        { get; set; }
    public string  Description { get; set; }

    public override string ToString() {
        return $"Adapter: {Name} - {Description} - {Message}";
    }
}