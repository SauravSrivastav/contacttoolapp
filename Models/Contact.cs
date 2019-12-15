using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactToolApp.Models
{
  [BsonIgnoreExtraElements]
  public class Contact
  {
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("firstName")]
    public string FirstName { get; set; }

    [BsonElement("lastName")]
    public string LastName { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }
  }
}