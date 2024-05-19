namespace DCCRailway.Common.Helpers;

public class Result(bool success, string message = "", Exception? exception = null) : IResult {

    // Constructors
    // -----------------------------------------------------------------------
    public Result() : this(true) { }
    public Result(IResult result) : this(result.Success, result.Message, result.Exception) { }

    // Data
    // -----------------------------------------------------------------------
    public bool       Success   { get; protected init; } = success;
    public string     Message   { get; protected init; } = message;
    public Exception? Exception { get; protected init; } = exception;

    // Helpers
    // -----------------------------------------------------------------------
    public static IResult Ok()                      => new Result();
    public static IResult Ok(IResult result)        => new Result(true, result.Message, result.Exception);
    public static IResult Ok(string errorMessage)   => new Result(true,errorMessage,null);

    public static IResult Fail()                    => new Result(false);
    public static IResult Fail(string message)      => new Result(true, message);
    public static IResult Fail(Exception ex)        => new Result(true, ex.Message, ex);
    public static IResult Fail(IResult result)      => new Result(false, result.Message, result.Exception);
    public static IResult Fail(string message, Exception ex) => new Result(true, message, ex);

    public override string ToString() => $"({(Success ? "Success" : "Failed")}) {Message}";
}