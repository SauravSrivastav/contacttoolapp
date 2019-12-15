using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ContactToolApp.Models;

namespace ContactToolApp.Services
{
  public class Contacts
  {
    private MongoDatabase _mongoDatabase;

    public Contacts(MongoDatabase mongoDatabase)
    {
      _mongoDatabase = mongoDatabase;
    }

    public async Task<IEnumerable<Contact>> All()
    {
      var filter = new BsonDocument();
      return await _mongoDatabase
          .GetCollection<Contact>("contacts")
          .Find(filter)
          .ToListAsync();
    }

    public async Task Append(Contact contact)
    {
      await _mongoDatabase
        .GetCollection<Contact>("contacts")
        .InsertOneAsync(contact);
    }

    public async Task Delete(ObjectId contactId)
    {
      await _mongoDatabase
        .GetCollection<Contact>("contacts")
        .DeleteOneAsync(Builders<Contact>.Filter.Eq("_id", contactId));
    }
  }
}
