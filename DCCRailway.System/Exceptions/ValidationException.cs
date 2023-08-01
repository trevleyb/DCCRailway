using System;

namespace DCCRailway.System.Exceptions;

public class ValidationException : Exception {
    public ValidationException(string? message) : base(message) { }
}