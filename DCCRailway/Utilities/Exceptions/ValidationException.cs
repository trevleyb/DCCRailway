﻿namespace DCCRailway.System.Utilities.Exceptions;

public class ValidationException : Exception {
    public ValidationException(string? message) : base(message) { }
}