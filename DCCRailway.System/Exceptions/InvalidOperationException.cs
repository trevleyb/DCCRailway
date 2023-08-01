using System;

namespace DCCRailway.System.Exceptions;

public class InvalidOperationException : Exception {
    public InvalidOperationException(string? message) : base(message) { }
}