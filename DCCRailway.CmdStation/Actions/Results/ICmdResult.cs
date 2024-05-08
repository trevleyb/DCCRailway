namespace DCCRailway.CmdStation.Commands.Results;

public interface ICmdResult {
    bool Success { get; }
    string? ErrorMessage { get; }
    ICommand? Command { get; set; }

    byte[] Data { get; }
    public byte Byte { get; }
    public byte this[int index] { get; }
    public int  Length { get; }
    public IEnumerable<byte> ToBytes();
}