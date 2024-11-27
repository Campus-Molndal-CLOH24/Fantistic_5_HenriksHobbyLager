using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;

namespace HenriksHobbyLager.Factories
{
    internal class ProductRepositoryFactory : IRepositoryFactory<Product>
    {
        public IRepository<Product> CreateRepository(string type)
        {
            if (type.ToLower() == "sqlite")
            {
                var dbContext = new SqliteDbcontext();
                dbContext.EnsureProductsTableExists();
                return new SQLiteProductRepository(dbContext);
            }
            else if (type.ToLower() == "mongodb")
            {
                DotNetEnv.Env.Load();
                //string connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION");
                string connectionString = "mongodb://localhost:27017/";
                Console.WriteLine($"string = '{connectionString}'");
                var dbName = "HenriksHobbyLager";
                var dbContext = new MongoDbcontext(connectionString, dbName);
                dbContext.EnsureProductsCollectionExists();
                return new MongoDBProductRepository(dbContext);
            }
            else
            {
                throw new ArgumentException("Invalid repository type");
            }
        }

        IRepository<Product> IRepositoryFactory<Product>.CreateRepository(string type)
        {
            throw new NotImplementedException();
        }
    }
}
