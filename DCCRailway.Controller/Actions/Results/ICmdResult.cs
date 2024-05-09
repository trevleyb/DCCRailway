namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResult {
    bool Success { get; }
    string? ErrorMessage { get; }
    ICommand? Command { get; set; }

    byte[] Data { get; }
    byte Byte { get; }
    byte this[int index] { get; }
    int  Length { get; }
    IEnumerable<byte> ToBytes();
}