namespace DCCRailway.Layout.Controllers.Events;

public class ControllerEventArgs(string message = "") : EventArgs, IControllerEventArgs {
    public string Message { get; set; } = message;
}