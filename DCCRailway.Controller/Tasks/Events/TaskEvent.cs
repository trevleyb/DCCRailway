namespace DCCRailway.Controller.Tasks.Events;

public class TaskEvent(IControllerTask? task) : EventArgs, ITaskEvent {
    public IControllerTask Task { get; } = task;
}