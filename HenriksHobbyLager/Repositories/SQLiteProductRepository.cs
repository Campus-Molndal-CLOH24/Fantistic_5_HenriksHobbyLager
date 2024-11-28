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
            try
            {                      
            return await _sqliteDbContext.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving products. Please try again.", ex);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await _sqliteDbContext.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving product. Please try again.", ex);
            }
        }

        public async Task AddAsync(Product entity)
        {
            try
            {
                entity.Created = DateTime.Now;
                await _sqliteDbContext.Products.AddAsync(entity);
                await _sqliteDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occured while adding the product to the database. Please try again.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured. Please contact support.", ex);
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            try
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
                else
                {
                    throw new Exception("Product not found.");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occured while updating the product. Please try again.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured. Please contact support.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {            
                var product = await _sqliteDbContext.Products.FindAsync(id);
                if (product != null)
                {
                    _sqliteDbContext.Products.Remove(product);
                    await _sqliteDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Product not found.");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occured while deleting the product. Please try again.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occured. Please contact support.", ex);
            }
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            try
            {
                // EF Core does not support Func<T, bool> directly for filtering in the database,
                // so this is evaluated in memory.
                var products = await _sqliteDbContext.Products.ToListAsync();
                return products.Where(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while searching for products. Please try again.", ex);
            }
        }
    }
}