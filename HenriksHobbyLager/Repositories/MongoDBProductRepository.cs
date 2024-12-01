using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;

namespace HenriksHobbyLager.Repositories
{
    public class MongoDBProductRepository : IRepository<Product>
    {
        private readonly MongoDbcontext _mongoDbContext;
        private readonly TimeSpan _timeSpan;

        public MongoDBProductRepository(MongoDbcontext dbContext, float timeOut)
        {
            _mongoDbContext = dbContext;
            _timeSpan = TimeSpan.FromSeconds(timeOut);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    return await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync(cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod vid hämtning av produkter. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    return await _mongoDbContext.Products.Find(product => product.Id == id).FirstOrDefaultAsync(cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod när produkten skulle hämtas. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
            
        }

        public async Task AddAsync(Product entity)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    entity.Id = await GetLastIdAsync() + 1;
                    entity.Created = DateTime.Now;
                    await _mongoDbContext.Products.InsertOneAsync(entity, cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod när produkten skulle läggas till. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task UpdateAsync(Product entity)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    await _mongoDbContext.Products.FindOneAndReplaceAsync(product => product.Id == entity.Id, entity,
                        new FindOneAndReplaceOptions<Product> { }, cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ett fel uppstod vid uppdatering av produkten med ID: {entity.Id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task DeleteAsync(int id)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    await _mongoDbContext.Products.DeleteOneAsync(product => product.Id == id, cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Kunde inte radera produkten med ID: {id}. Vänligen försök igen eller kontakta supporten.", ex);
                }
            }
            
        }

        public async Task<IEnumerable<Product>> SearchAsync(Func<Product, bool> predicate)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
            {
                try
                {
                    var products = await _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToListAsync(cancellationTokenSource.Token);
                    return products.Where(predicate);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ett fel uppstod vid produktsökning. Vänligen försök igen eller kontakta supporten.");
                }
            }
            
        }

        private async Task<int> GetLastIdAsync()
        {
            using (var cancellationTokenSource = new CancellationTokenSource(_timeSpan))
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
}