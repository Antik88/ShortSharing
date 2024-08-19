namespace ShortSharing.BLL.Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string name)
        :base($"Access to {name} is forbidden")
    {
    }
}
