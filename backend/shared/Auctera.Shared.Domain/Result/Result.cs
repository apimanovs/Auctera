namespace Auctera.Shared.Domain.Results;

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorCode { get; }
    public string? Message { get; }

    private Result(bool isSuccess, T? value, string? errorCode, string? message)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorCode = errorCode;
        Message = message;
    }

    public static Result<T> Success(T value)
    {
        return new(true, value, null, null);
    }
    public static Result<T> Fail(string errorCode, string message)
    {
        return new(false, default, errorCode, message);
    }
}
