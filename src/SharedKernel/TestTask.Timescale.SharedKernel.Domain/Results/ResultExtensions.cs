namespace TestTask.Timescale.SharedKernel.Domain.Results
{
    public static class ResultExtensions
    {
        extension(Result result)
        {
            public bool IsFailure => !result.IsSuccess;

            public static Result Fail(Error error) => Result.Fail([error]);

            public static Result<T> Fail<T>(Error error) => new([error]);
        }
    }
}
