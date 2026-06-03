using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.Models;
public class Result
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public ErrorResultType ErrorResultType { get; init; } = ErrorResultType.None;

    public static Result Success(string message)
    {
        return new Result
        {
            IsSuccess = true,
            Message = message
        };
    }
    public static Result Failure(string message, ErrorResultType errorResultType)
    {
        return new Result
        { 
            IsSuccess = false, 
            Message = message, 
            ErrorResultType = errorResultType 
        };
    }
}

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public ErrorResultType ErrorResultType { get; init; } = ErrorResultType.None;

    public static Result<T> Success(T data, string message)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message
        };
    }
    public static Result<T> Failure(string message, ErrorResultType errorResultType) 
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message,
            ErrorResultType = errorResultType
        };
    }
}
