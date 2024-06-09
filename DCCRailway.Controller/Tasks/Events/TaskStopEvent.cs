namespace DCCRailway.Controller.Tasks.Events;

public class TaskStopEvent(IControllerTask? task) : TaskEvent(task) { }