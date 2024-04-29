namespace DCCRailway.Common.Results;

public class Result : IResult {
    public bool    IsSuccess { get; protected set; }
    public bool    IsFailure => !IsSuccess;
    public bool    IsOK      => IsSuccess;
    public string? Error     { get; }

    protected Result(bool isSuccess, string? error) {
        IsSuccess = isSuccess;
        Error     = error;
    }

    public static Result Success() => new(true, null);

    public static Result Fail(string? error) => new(false, error);
}

public class Result<T> : Result, IResult {
    protected T? Value { get; }

    protected Result(bool isSuccess, T? value, string? error) : base(isSuccess, error) => Value = value;

    public static Result<T> Success(T value) => new(true, value, null);

    public new static Result<T> Fail(string? error) => new(false, default(T?), error);

    public static Result<T> Fail(string? error, T value) => new(false, value, error);
}