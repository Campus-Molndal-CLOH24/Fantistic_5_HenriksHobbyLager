using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;

namespace HenriksHobbyLager.Facades
{
    internal class ProductFacade : IProductFacade
    {
        private readonly IRepository<Product> _repository;

        public ProductFacade(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _repository.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            var products = await _repository.GetAllAsync();
            return products.Where(product =>
                product.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                product.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }
    }
}