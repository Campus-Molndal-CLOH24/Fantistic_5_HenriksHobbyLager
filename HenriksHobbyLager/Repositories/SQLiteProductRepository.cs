using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriksHobbyLager.Repositories
{
    public class SQLiteProductRepository : IRepository<Product>
    {
        

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
            {
                try
                {                      
                    return await _sqliteDbContext.Products.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod vid hämtning av produkter. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
            {
                try
                {
                    return await _sqliteDbContext.Products.FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod när produkten skulle hämtas. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task AddAsync(Product entity)
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
            {
                try
                {
                    entity.Created = DateTime.Now;
                    await _sqliteDbContext.Products.AddAsync(entity);
                    await _sqliteDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod när produkten skulle läggas till. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task UpdateAsync(Product entity)
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
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
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception($"Ett fel uppstod vid uppdatering av produkten med ID: {entity.Id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ett fel uppstod vid uppdatering av produkten med ID: {entity.Id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task DeleteAsync(int id)
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
            {
                try
                {            
                    var product = await _sqliteDbContext.Products.FindAsync(id);
                    if (product != null)
                    {
                        _sqliteDbContext.Products.Remove(product);
                        await _sqliteDbContext.SaveChangesAsync();
                    }                   
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception($"Kunde inte radera produkten med ID: {id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Kunde inte radera produkten med ID: {id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            using (var _sqliteDbContext = new SqliteDbcontext())
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
                    throw new Exception("Ett fel uppstod vid produktsökning. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }
    }
}