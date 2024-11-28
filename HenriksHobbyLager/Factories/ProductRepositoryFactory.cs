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
                using (var dbContext = new SqliteDbcontext())
                {
                    dbContext.EnsureProductsTableExists();
                }
                return new SQLiteProductRepository();
            }
            else if (type.ToLower() == "mongodb")
            {
                DotNetEnv.Env.Load();
                string connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION");
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
