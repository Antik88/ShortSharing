namespace ShortSharing.Shared;

public class QueryParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CategoryId { get; set; }
    public Guid? TypeId { get; set; }
}
