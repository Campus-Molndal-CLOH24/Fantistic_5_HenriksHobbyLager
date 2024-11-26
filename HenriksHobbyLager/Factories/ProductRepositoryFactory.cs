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
                return new SQLiteProductRepository(dbContext);
            }
            else if (type.ToLower() == "mongodb")
            {
                return null; //new MongoDBProductRepository();
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
