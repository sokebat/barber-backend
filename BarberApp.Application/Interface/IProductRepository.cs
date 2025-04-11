using BarberApp.Domain.Models;

namespace BarberApp.Application.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task DeleteProduct(int id);
        Task UpdateProduct(Product product);
    }
}
