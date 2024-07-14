namespace ShortSharing.Shared;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages
    {
        get
        {
            return (int)Math.Ceiling((double)TotalCount / PageSize);
        }
    }

    public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        CurrentPage = pageNumber;
        PageSize = pageSize;
    }
}
