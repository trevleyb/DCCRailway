namespace DCCRailway.System.Commands.Results;

public class ResultBase : IResult {
    public ResultBase(byte[]? data = null) => Data = data;

    public ResultBase(byte? data = null) => Data = data == null ? Array.Empty<byte>() : new[] { (byte)data };

    public byte[]? Data { get; }

    public int Bytes => Data?.Length ?? 0;

    public byte? Value => Data?[0];

    public bool OK => true;
}