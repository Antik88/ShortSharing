namespace Rent.Service.Application.Abstractions;

public interface IRentAvailabilityRepository
{
    Task<bool> IsAvailableAsync(Guid thingId, DateTime startRentDate, DateTime endRentDate);
    Task<bool> IsAvailableForExtensionAsync(Guid rentId, DateTime newEndRentDate);
}
