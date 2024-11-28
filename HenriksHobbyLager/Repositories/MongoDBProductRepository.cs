﻿using HenriksHobbyLager.Database;
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
            try
            {
                return await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod vid hämtning av produkter. Vänligen försök igen eller kontakta supporten.", ex);
            }

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await _mongoDbContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när produkten skulle hämtas. Vänligen försök igen eller kontakta supporten.", ex);
            }
            
        }

        public async Task AddAsync(Product entity)
        {
            try
            {
                entity.Id = await GetLastIdAsync() + 1;
                entity.Created = DateTime.Now;
                await _mongoDbContext.Products.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när produkten skulle läggas till. Vänligen försök igen eller kontakta supporten.", ex);
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            try
            {
                await _mongoDbContext.Products.FindOneAndReplaceAsync(product => product.Id == entity.Id, entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod vid uppdatering av produkten med ID: {entity.Id}. Vänligen försök igen eller kontakta supporten.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _mongoDbContext.Products.DeleteOneAsync(product => product.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kunde inte radera produkten med ID: {id}. Vänligen försök igen eller kontakta supporten.", ex);
            }
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            try
            {
                var products = await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return products.Where(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod vid produktsökning. Vänligen försök igen eller kontakta supporten.");
            }
        }

        private async Task<int> GetLastIdAsync()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Kunde inte skapa korrekt ID. Vänligen försök igen eller kontakta supporten.");
            }
        }
    }
}