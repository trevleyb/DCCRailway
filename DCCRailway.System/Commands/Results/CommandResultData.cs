namespace DCCRailway.System.Commands.Results;

public class CommandResultData(byte[]? data = null) {
    public CommandResultData(byte? data = null) : this(data == null ? Array.Empty<byte>() : new[] { (byte)data }) { }
    public byte this[int           index] => Data?[index] ?? 0;
    public byte[]?           Data      { get; } = data ?? Array.Empty<byte>();
    public int               Length    => Data?.Length ?? 0;
    public IEnumerable<byte> ToBytes() => Data ?? Array.Empty<byte>();
}