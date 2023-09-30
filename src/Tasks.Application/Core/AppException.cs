namespace Tasks.Application.Core;

public class AppException : Exception
{
    public Error Error { get; }

    public AppException(Error error) : base(error.Description)
    {
        Error = error;
    }

    public AppException(Error error, Exception innerException) : base(error.Description, innerException)
    {
        Error = error;
    }
}

