namespace Application.Core;

public class ServiceException : Exception
{
    public Error Error { get; }

    public ServiceException(Error error) : base(error.Description)
    {
        Error = error;
    }

    public ServiceException(Error error, Exception innerException) : base(error.Description, innerException)
    {
        Error = error;
    }
}

