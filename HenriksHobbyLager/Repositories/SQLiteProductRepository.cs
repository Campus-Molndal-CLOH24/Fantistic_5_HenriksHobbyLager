using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;


namespace HenriksHobbyLager.Repositories;

public class SQLiteProductRepository : IRepository<Product>
{
    private readonly SqliteDbcontext _sqliteSqlDbContext;


    public SQLiteProductRepository(SqliteDbcontext dbContext)
    {
        _sqliteSqlDbContext = dbContext;
    }
    public IEnumerable<Product> GetAll()
    {
        return _sqliteSqlDbContext.Products.ToList();
    }

    public Product GetById(int id)
    {
        return _sqliteSqlDbContext.Products.Find(id);
    }

    public void Add(Product entity)
    {
        entity.Created = DateTime.Now;
        _sqliteSqlDbContext.Products.Add(entity);
        _sqliteSqlDbContext.SaveChanges();
    }

    public void Update(Product entity)
    {
        var existingProduct = _sqliteSqlDbContext.Products.Find(entity.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.Stock = entity.Stock;
            existingProduct.Category = entity.Category;
            existingProduct.LastUpdated = DateTime.Now;

            _sqliteSqlDbContext.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var product = _sqliteSqlDbContext.Products.Find(id);
        if (product != null)
        {
            _sqliteSqlDbContext.Products.Remove(product);
            _sqliteSqlDbContext.SaveChanges();
        }
    }

    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        return _sqliteSqlDbContext.Products.Where(predicate).ToList();
    }
}