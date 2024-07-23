namespace Rent.Service.Application.Common.Constants;

public class ValidationMessages
{
    public const string StartDateRequired = "Start date is required";
    public const string StartDateInvalid = "Start date must be a valid date";
    public const string StartDateInFuture = "Start date must be in the future";
    public const string EndDateRequired = "End date is required";
    public const string EndDateInvalid = "End date must be a valid date";
    public const string EndDateAfterStartDate = "End date must be after the start date";
    public const string ThingIdRequired = "Thing ID is required";
    public const string ThingIdInvalid = "Thing ID must be a valid GUID";
    public const string UserIdRequired = "User ID is required";
    public const string UserIdInvalid = "User ID must be a valid GUID";
}
