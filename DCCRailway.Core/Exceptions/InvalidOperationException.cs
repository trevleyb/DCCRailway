using System;

namespace DCCRailway.Core.Exceptions; 

public class InvalidOperationException : Exception {
    public InvalidOperationException(string? message) : base(message) { }
}