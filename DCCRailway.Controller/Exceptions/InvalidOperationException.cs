namespace DCCRailway.Controller.Exceptions;

public class InvalidOperationException : Exception {
    public InvalidOperationException(string? message) : base(message) { }
}