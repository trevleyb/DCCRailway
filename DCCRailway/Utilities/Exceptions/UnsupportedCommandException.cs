namespace DCCRailway.System.Utilities.Exceptions;

public class UnsupportedCommandException : Exception {
    public UnsupportedCommandException(string? message) : base(message) { }
}