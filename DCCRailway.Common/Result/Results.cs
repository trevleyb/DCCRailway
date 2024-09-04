namespace DCCRailway.Common.Result;

public abstract class Results {
    public bool Success { get; protected init; }
    public bool Failure => !Success;
}

public abstract class Results<T> : Results {
    private readonly T? _data;

    protected Results(T? data) {
        Data = data;
    }

    public T? Data {
        get => Success ? _data : throw new Exception($"You can't access .{nameof(Data)} when .{nameof(Success)} is false");
        init => _data = value;
    }
}

public class SuccessResult : Results {
    public SuccessResult() {
        Success = true;
    }
}

public class SuccessResult<T> : Results<T> {
    public SuccessResult(T data) : base(data) {
        Success = true;
    }
}

public class ErrorResult : Results, IErrorResult {
    public ErrorResult(Exception exception) : this(exception.Message) { }
    public ErrorResult(string message) : this(message, Array.Empty<Error>()) { }

    public ErrorResult(string message, IReadOnlyCollection<Error> errors) {
        Message = message;
        Success = false;
        Errors  = errors;
    }

    public string                     Message { get; }
    public IReadOnlyCollection<Error> Errors  { get; }
}

public class ErrorResult<T> : Results<T>, IErrorResult {
    public ErrorResult(Exception exception) : this(exception.Message, Array.Empty<Error>(), exception) { }
    public ErrorResult(string message, Exception exception) : this(message, Array.Empty<Error>(), exception) { }
    public ErrorResult(string message) : this(message, Array.Empty<Error>(), null) { }

    public ErrorResult(string message, IReadOnlyCollection<Error>? errors, Exception? exception) : base(default) {
        Message   = message;
        Success   = false;
        Errors    = errors ?? Array.Empty<Error>();
        Exception = exception;
    }

    public Exception? Exception { get; }

    public string                     Message { get; set; }
    public IReadOnlyCollection<Error> Errors  { get; }
}

public class Error(string code, string details) {
    public Error(string details) : this("", details) { }
    public string Code    { get; } = code;
    public string Details { get; } = details;
}

internal interface IErrorResult {
    string                     Message { get; }
    IReadOnlyCollection<Error> Errors  { get; }
}