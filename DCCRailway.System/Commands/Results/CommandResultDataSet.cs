using DCCRailway.System.Utilities;

namespace DCCRailway.System.Commands.Results;

/// <summary>
/// Represents a Raw Data Set that has been returned from a command.
/// </summary>
public class CommandResultDataSet {
    public CommandResultDataSet(byte[]? data = null) => Data = data;
    public byte[]? Data         { get; }
    public string? DataAsString => Data.ToDisplayValues() ?? string.Empty;
    public int     Bytes        => Data?.Length ?? 0;
    public byte?   Value        => Data?[0];
    public int this[int index]  => Data?[index] ?? 0;
}