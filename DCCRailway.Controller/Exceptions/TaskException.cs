using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Tasks;

namespace DCCRailway.Controller.Exceptions;

public class TaskException : Exception {
    public TaskException(string? taskName, string? message, Exception? ex = null) : base(taskName + ":" + message, ex) { }
}