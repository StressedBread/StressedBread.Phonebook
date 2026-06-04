using static StressedBread.Phonebook.Enums;

namespace StressedBread.Phonebook.Models;
public class Result
{
    public bool IsSuccess { get; init; }
    public ResultType ResultType { get; init; } = ResultType.None;

    public static Result Success(ResultType resultType)
    {
        return new Result
        {
            IsSuccess = true,
            ResultType = resultType
        };
    }

    public static Result Failure(ResultType resultType)
    {
        return new Result
        { 
            IsSuccess = false, 
            ResultType = resultType 
        };
    }

    public static Result<T> Failure<T>(ResultType resultType)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ResultType = resultType
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    public static Result<T> Success(T data, ResultType resultType)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            ResultType = resultType
        };
    }
}
