namespace DCCRailway.Common.Helpers;

public class Result(bool success, string message = "", Exception? exception = null) : IResult {
    // Constructors
    // -----------------------------------------------------------------------
    public Result() : this(true) { }
    public Result(IResult result) : this(result.Success, result.Message, result.Exception) { }

    // Data
    // -----------------------------------------------------------------------
    public bool Success { get; protected init; } = success;

    public bool Failed => !Success;

    public string     Message   { get; protected init; } = message;
    public Exception? Exception { get; protected init; } = exception;

    // Helpers
    // -----------------------------------------------------------------------
    public static IResult Ok() {
        return new Result();
    }

    public static IResult Ok(IResult result) {
        return new Result(true, result.Message, result.Exception);
    }

    public static IResult Ok(string errorMessage) {
        return new Result(true, errorMessage, null);
    }

    public static IResult Fail() {
        return new Result(false);
    }

    public static IResult Fail(string message) {
        return new Result(false, message);
    }

    public static IResult Fail(Exception ex) {
        return new Result(false, ex.Message, ex);
    }

    public static IResult Fail(IResult result) {
        return new Result(false, result.Message, result.Exception);
    }

    public static IResult Fail(string message, Exception ex) {
        return new Result(false, message, ex);
    }

    public override string ToString() {
        return $"({(Success ? "Success" : "Failed")}) {Message}";
    }
}