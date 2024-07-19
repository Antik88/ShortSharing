namespace User.Service.Shared;

public class PageResult<T>
{
    public List<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
