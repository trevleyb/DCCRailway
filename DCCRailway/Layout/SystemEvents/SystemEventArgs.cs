namespace DCCRailway.Layout.SystemEvents;

public abstract class SystemEventArgs {
    public SystemEventType Type { get; set; }
    public SystemEventAction Action { get; set; }
}