namespace ShortSharing.API.Dtos.ThingDtos; 

public record ThingDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string CategoryName { get; set; }
    public string TypeName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
