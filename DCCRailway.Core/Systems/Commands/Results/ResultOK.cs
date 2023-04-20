using System;

namespace DCCRailway.Core.Systems.Commands.Results; 

public class ResultOK : ResultBase, IResultOK, IResult {
    public ResultOK() : base(Array.Empty<byte>()) { }

    public ResultOK(byte? data = null) : base(data) { }

    public ResultOK(byte[]? data = null) : base(data) { }

    public new bool OK => true;
}