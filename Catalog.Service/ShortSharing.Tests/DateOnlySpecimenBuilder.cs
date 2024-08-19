using AutoFixture.Kernel;

namespace ShortSharing.Tests;

public class DateOnlySpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(DateOnly))
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }

        return new NoSpecimen();
    }
}
