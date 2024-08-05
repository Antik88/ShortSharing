using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Email.Service.BLL.Models;

public class Template
{

    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("body")]
    public string Body { get; set; } = string.Empty;
}
