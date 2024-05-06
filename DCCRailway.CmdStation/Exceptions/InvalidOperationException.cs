namespace DCCRailway.CmdStation.Exceptions;

public class InvalidOperationException : Exception {
    public InvalidOperationException(string? message) : base(message) { }
}