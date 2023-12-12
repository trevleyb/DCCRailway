namespace DCCRailway.System.Commands.Result;

public interface IResult {
    public bool    OK    { get; }
    public int     Bytes { get; }
    public byte[]? Data  { get; }
    public byte?   Value { get; }
}