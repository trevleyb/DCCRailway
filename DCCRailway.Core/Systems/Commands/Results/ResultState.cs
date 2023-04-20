using System;

namespace DCCRailway.Core.Systems.Commands.Results; 

public class ResultState : ResultBase, IResultState, IResultOK, IResult {
    public ResultState(bool? state) : base(Array.Empty<byte>()) {
        State = state;
    }

    public ResultState(bool? state, byte[]? data = null) : base(data) {
        State = state;
    }

    public ResultState(byte[]? data = null) : base(data) {
        State = null;
    }

    public new bool OK => true;

    public bool? State { get; }
}