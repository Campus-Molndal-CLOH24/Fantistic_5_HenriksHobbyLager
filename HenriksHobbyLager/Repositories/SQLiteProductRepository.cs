using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Repositories
{
    public class SQLiteProductRepository : IRepository<Product>
    {
        private readonly SqliteDbcontext _sqliteDbContext;

        public SQLiteProductRepository(SqliteDbcontext dbContext)
        {
            _sqliteDbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _sqliteDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _sqliteDbContext.Products.FindAsync(id);
        }

        public async Task AddAsync(Product entity)
        {
            entity.Created = DateTime.Now;
            await _sqliteDbContext.Products.AddAsync(entity);
            await _sqliteDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            var existingProduct = await _sqliteDbContext.Products.FindAsync(entity.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = entity.Name;
                existingProduct.Price = entity.Price;
                existingProduct.Stock = entity.Stock;
                existingProduct.Category = entity.Category;
                existingProduct.LastUpdated = DateTime.Now;

                await _sqliteDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _sqliteDbContext.Products.FindAsync(id);
            if (product != null)
            {
                _sqliteDbContext.Products.Remove(product);
                await _sqliteDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            // EF Core does not support Func<T, bool> directly for filtering in the database,
            // so this is evaluated in memory.
            var products = await _sqliteDbContext.Products.ToListAsync();
            return products.Where(predicate);
        }
    }
}