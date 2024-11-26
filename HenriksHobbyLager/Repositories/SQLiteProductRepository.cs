using HenriksHobbyLager.Database;
using HenriksHobbyLager.Models;
using RefactoringExercise.Interfaces;

namespace HenriksHobbyLager.Repositories;

public class SQLiteProductRepository : IRepository<Product>
{
    private readonly SqliteDbcontext _sqliteSqlDbContext;


    public SQLiteProductRepository(SqliteDbcontext sqlDbContext)
    {
        _sqliteSqlDbContext = sqlDbContext;
    }
    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Product entity)
    {
        _sqliteSqlDbContext.Products.Add(entity);
        _sqliteSqlDbContext.SaveChanges();
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Search(Func<Product, bool> predicate)
    {
        throw new NotImplementedException();
    }
}