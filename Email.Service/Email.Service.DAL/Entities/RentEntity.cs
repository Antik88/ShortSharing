using Email.Service.DAL.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Email.Service.DAL.Entities;

public class RentEntity
{
    [BsonId]
    [BsonElement("_id")]
    public Guid Id { get; set; }

    [BsonElement("start_rent_date")]
    public DateTime StartDate { get; set; }

    [BsonElement("end_rent_date")]
    public DateTime EndDate { get; set; }

    [BsonElement("thingName")]
    public string ThingName { get; set; } = string.Empty;

    [BsonElement("owner")]
    public string OwnerEmail { get; set; } = string.Empty;

    [BsonElement("owner_name")]
    public string OwnerName { get; set; } = string.Empty;

    [BsonElement("tenant")]
    public string TenantEmail { get; set; } = string.Empty;

    [BsonElement("tenant_name")]
    public string TenantName { get; set; } = string.Empty;

    [BsonElement("status")]
    public RentStatus Status { get; set; } = RentStatus.Pending;
}
