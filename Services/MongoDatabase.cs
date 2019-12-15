using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ContactToolApp.Services
{
  public class MongoDatabase
  {
    private IConfiguration _configuration;
    private MongoClient _mongoClient;
    private IMongoDatabase _mongoDatabase;
    public MongoDatabase(IConfiguration configuration)
    {
      _configuration = configuration;

      MongoClientSettings settings = MongoClientSettings.FromUrl(
        new MongoUrl(_configuration["ConnectionString"])
      );

      settings.SslSettings = new SslSettings()
      {
        EnabledSslProtocols = SslProtocols.Tls12
      };

      _mongoClient = new MongoClient(settings);
      _mongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
      return _mongoDatabase.GetCollection<T>(collectionName);
    }
  }
}
