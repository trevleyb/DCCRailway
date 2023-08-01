using System;

namespace DCCRailway.System.Exceptions;

public class UnsupportedCommandException : Exception {
    public UnsupportedCommandException(string? message) : base(message) { }
}