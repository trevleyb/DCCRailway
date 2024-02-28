namespace DCCRailway.Common.Utilities.Results;

public class Result : IResult
{
    public bool    IsSuccess { get; protected set; }
    public bool    IsFailure => !IsSuccess;
    public bool    IsOK      => IsSuccess;
    public string? Error     { get; }

    protected Result(bool isSuccess, string? error) {
        IsSuccess = isSuccess;
        Error     = error;
    }

    public static Result Success() {
        return new Result(true, null);
    }

    public static Result Fail(string? error) {
        return new Result(false, error);
    }
}

public class Result<T> : Result, IResult {
    protected T? Value { get; }

    protected Result(bool isSuccess, T? value, string? error) : base(isSuccess, error) {
        Value = value;
    }

    public static Result<T> Success(T value) {
        return new Result<T>(true, value, null);
    }

    public new static Result<T> Fail(string? error) {
        return new Result<T>(false, default, error);
    }

    public static Result<T> Fail(string? error, T value) {
        return new Result<T>(false, value, error);
    }

}