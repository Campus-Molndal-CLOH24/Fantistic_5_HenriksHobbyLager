using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Bson;
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
        
        public IEnumerable<Product> GetAll()
        {
            return _mongoDbContext.Products.Find(FilterDefinition<Product>.Empty).ToList();

        }

        public Product GetById(int id)
        {
            return _mongoDbContext.Products.Find(product => product.Id == id).FirstOrDefault();
        }

        public void Add(Product entity)
        {
            entity.Id = GetLastId() + 1;
            entity.Created = DateTime.Now;
            _mongoDbContext.Products.InsertOne(entity);
        }

        public void Update(Product entity)
        {
            /*var existingProduct = _mongoDbContext.Products.Find(entity.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = entity.Name;
                existingProduct.Price = entity.Price;
                existingProduct.Stock = entity.Stock;
                existingProduct.Category = entity.Category;
                existingProduct.LastUpdated = DateTime.Now;

                _mongoDbContext.SaveChanges();
            }*/
        }

        public void Delete(int id)
        {
            _mongoDbContext.Products.DeleteOne(product => product.Id == id);
        }

        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            //return _mongoDbContext.Products.Where(predicate).ToList();
            return null;
        }

        private int GetLastId()
        {
            var products = GetAll();

            if (products.Any())
            {
                List<int> idList = new List<int>();
                foreach (var product in products)
                {
                    idList.Add(product.Id);
                }
            
                idList.Sort();

                return idList[idList.Count - 1];
            }
            else
            {
                return 0;
            }
            
        }
    }
}
