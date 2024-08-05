namespace Rent.Service.Application.Common.Constants;

public static class ValidationMessages
{
    public const string StartDateRequired = "Start date is required";
    public const string StartDateInvalid = "Start date must be a valid date";
    public const string StartDateInFuture = "Start date must be in the future";
    public const string EndDateRequired = "End date is required";
    public const string EndDateInvalid = "End date must be a valid date";
    public const string EndDateAfterStartDate = "End date must be after the start date";
    public const string ThingIdRequired = "Thing ID is required";
    public const string ThingIdInvalid = "Thing ID must be a valid GUID";
    public const string RentIdInvalid = "Rent ID must be a valid GUID.";
    public const string UserIdRequired = "User ID is required";
    public const string UserIdInvalid = "User ID must be a valid GUID";
    public const string NotAvailableToRent = "The thing is not available for rent in the specified dates";
    public const string NotAvailableToExtend = "The rental period cannot be extended due to conflicts with existing rentals.";
    public const string ThingNotFound = "Item not found in the catalog";
    public const string ServiceUrlNotFound  = "Service url not found";
}
