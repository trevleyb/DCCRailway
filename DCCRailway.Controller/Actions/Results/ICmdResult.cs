using DCCRailway.Common.Result;

namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResult : IResult {
    ICommand? Command { get; set; }

    byte[] Data { get; }
    byte   Byte { get; }
    byte this[int index] { get; }
    int               Length { get; }
    IEnumerable<byte> ToBytes();
}