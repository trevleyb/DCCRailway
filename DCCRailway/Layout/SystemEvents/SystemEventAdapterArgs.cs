using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Layout.SystemEvents;

public class SystemEventAdapterArgs : SystemEventArgs {
    public SystemEventAdapterArgs(IAdapter adapter, SystemEventAction action, string message) {
        Type        = SystemEventType.Adapter;
        Action      = action;
        Message     = message;
        Name        = adapter.Info().Name;
        Description = adapter.Info().Description;
    }

    public string Message     { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    
    public override string ToString() {
        return $"Adapter: {Name} - {Description} - {Message}";
    }
}