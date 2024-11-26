using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Facades
{
    internal class ProductFacade : IProductFacade
    {
        private IRepository<Product> _repository;
        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public void CreateProduct(Product product)
        {
            _repository.Add(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public Product GetProduct(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _repository.Search(product =>
                product.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                product.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }
    }
}
