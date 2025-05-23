namespace DCCRailway.Controller.Triggers;

public interface ITrigger {
    DateTime? LastTriggered   { get; set; }
    int       TriggerInterval { get; set; }
    bool      IsTriggered     { get; set; }
}