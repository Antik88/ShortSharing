namespace Rent.Service.Application.Common.Exceptions;

public class InvalidRequestException : Exception
{
    public List<string> Errors { get; }

    public InvalidRequestException(List<string> errors)
    {
        Errors = errors; 
    }
}
