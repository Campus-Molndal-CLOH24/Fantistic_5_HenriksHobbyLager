using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;

namespace HenriksHobbyLager.Repositories
{
    public class MongoDBProductRepository : IRepository<Product>
    {
        private readonly MongoDbcontext _mongoDbContext;

        public MongoDBProductRepository(MongoDbcontext dbContext)
        {
            _mongoDbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _mongoDbContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product entity)
        {
            entity.Id = await GetLastIdAsync() + 1;
            entity.Created = DateTime.Now;
            await _mongoDbContext.Products.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _mongoDbContext.Products.FindOneAndReplaceAsync(product => product.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _mongoDbContext.Products.DeleteOneAsync(product => product.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            var products = await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync();
            return products.Where(predicate);
        }

        private async Task<int> GetLastIdAsync()
        {
            var products = await GetAllAsync();

            if (products.Any())
            {
                List<int> idList = products.Select(p => p.Id).ToList();
                idList.Sort();
                return idList[^1]; // Using C# index from end operator
            }
            else
            {
                return 0;
            }
        }
    }
}