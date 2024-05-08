namespace DCCRailway.Controller.Exceptions;

public class UnsupportedCommandException : Exception {
    public UnsupportedCommandException(string? message) : base(message) { }
}