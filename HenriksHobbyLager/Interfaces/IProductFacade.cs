using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Interfaces
{
    internal interface IProductFacade
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> SearchProducts(string searchTerm);
    }
}
