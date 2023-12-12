using DCCRailway.System.Utilities.Results;

namespace DCCRailway.System.Commands.Results;

public class CommandResultData {
    
    public CommandResultData(byte[]? data = null) => Data = data;
    public CommandResultData(byte?   data = null) => Data = data == null ? Array.Empty<byte>() : new[] { (byte)data };
    
    public byte this[int index] => Data?[index] ?? 0;
    public byte[]? Data { get; }
    public int Length => Data?.Length ?? 0;
    public IEnumerable<byte> ToBytes() => Data ?? Array.Empty<byte>();
}