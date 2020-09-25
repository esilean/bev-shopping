using BevShopping.Catalog.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BevShopping.Catalog.Data.Data
{
    public class CatalogDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly CatalogDbConfig _catalogDbConfig;

        public CatalogDbContext(IOptions<CatalogDbConfig> options)
        {
            _catalogDbConfig = options.Value;

            var client = new MongoClient(_catalogDbConfig.ConnectionString);
            _database = client.GetDatabase(_catalogDbConfig.Database);
        }

        public IMongoCollection<CatalogItem> CatalogItems => _database.GetCollection<CatalogItem>(_catalogDbConfig.Collection);
    }
}