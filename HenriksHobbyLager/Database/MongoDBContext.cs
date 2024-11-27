using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HenriksHobbyLager.Database
{
    public class MongoDbcontext : DbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbcontext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Hämtar collection för produkterna
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

        public void EnsureProductsCollectionExists()
        {
            // Kontrollera om "Products"-collection redan finns
            var collectionNames = _database.ListCollectionNames().ToList();
            if (!collectionNames.Contains("Products"))
            {
                // Skapa "Products"-collection
                _database.CreateCollection("Products");

                // Skapa index för egenskaper
                var productsCollection = Products;
                var indexKeys = Builders<Product>.IndexKeys;

                productsCollection.Indexes.CreateMany(new[]
                {
                    new CreateIndexModel<Product>(indexKeys.Ascending(p => p.Name), new CreateIndexOptions { Name = "idx_product_name" }),
                    new CreateIndexModel<Product>(indexKeys.Ascending(p => p.Category), new CreateIndexOptions { Name = "idx_product_category" }),
                    new CreateIndexModel<Product>(indexKeys.Ascending(p => p.Price), new CreateIndexOptions { Name = "idx_product_price" })
                });

                Console.WriteLine("Products collection and indexes created.");
            }
            else
            {
                Console.WriteLine("Products collection already exists.");
            }
        }
    }
}