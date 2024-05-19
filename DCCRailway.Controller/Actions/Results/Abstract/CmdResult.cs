using DCCRailway.Common.Helpers;

namespace DCCRailway.Controller.Actions.Results.Abstract;

public class CmdResult(bool success, ICommand? command, byte[]? data, string? errorMessage = null) : Result (success,errorMessage ?? ""), ICmdResult, IResult {

    // Constructors
    // -----------------------------------------------------------------------
    public CmdResult() : this(true, null, null, null) { }
    public CmdResult(bool success, string? errorMessage = null) : this(success, null, null, errorMessage) { }
    public CmdResult(ICommand command, byte[]? data) : this(true, command, data) { }
    public CmdResult(byte[]? data) : this(true, null, data) { }
    public CmdResult(bool success, byte[]? data, string? errorMessage = null) : this(success, null, data, errorMessage) { }
    public CmdResult(ICmdResult result) : this(result.Success, result.Command, result.Data, result.Message) { }

    // Data
    // -----------------------------------------------------------------------
    public byte[]    Data         { get; set; }           = data ?? [];
    public int       Length       => Data?.Length ?? 0;
    public ICommand? Command      { get; set; } = command;
    public byte      Byte         => Data.Length == 1 ? this[0] : (byte)0;

    public byte this[int index] => Data?[index] ?? 0;
    public IEnumerable<byte> ToBytes() => Data ?? [];

    // Helpers
    // -----------------------------------------------------------------------
    public new static ICmdResult Ok()                            => new CmdResult();
    public static ICmdResult Ok(ICommand? command)               => new CmdResult(true, command, null);
    public static ICmdResult Ok(byte[]? data)                    => new CmdResult(true, null, data);
    public static ICmdResult Ok(ICommand? command, byte[]? data) => new CmdResult(true, command, data);
    public static ICmdResult Ok(ICmdResult result)               => new CmdResult(true, result.Command, result.Data, "");

    public new static ICmdResult Fail()                                                 => new CmdResult();
    public static ICmdResult Fail(ICommand? command, string errorMessage)               => new CmdResult(true, command, null, errorMessage);
    public static ICmdResult Fail(byte[]? data, string errorMessage)                    => new CmdResult(true, null, data, errorMessage);
    public static ICmdResult Fail(ICommand? command, byte[]? data, string errorMessage) => new CmdResult(true, command, data, errorMessage);
    public new static ICmdResult Fail(string errorMessage)                                  => new CmdResult(true, null, null, errorMessage);
    public static ICmdResult Fail(ICmdResult result)                                    => new CmdResult(false, result.Command, result.Data, result.Message);

    public override string ToString() => $"({(Success ? "Success" : "Failed")}) {Message}";
}