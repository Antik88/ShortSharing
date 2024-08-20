using Email.Service.DAL.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Email.Service.BLL.Models;

public class TemplateModel
{
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("type")]
    public RentTemplateType Type { get; set; }

    [BsonElement("body")]
    public string Body { get; set; } = string.Empty;
}
