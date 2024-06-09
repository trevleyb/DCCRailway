namespace DCCRailway.Controller.Tasks.Events;

public class TaskStartEvent(IControllerTask? task) : TaskEvent(task) { }