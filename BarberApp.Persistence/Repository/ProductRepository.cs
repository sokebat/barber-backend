using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BarberApp.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(BarberDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "Product cannot be null.");

                // Ensure Id is not set (let SQL Server generate it)
                if (product.Id != 0)
                {
                    _logger.LogWarning("Attempted to add product with explicit Id {Id}. Resetting to 0.", product.Id);
                    product.Id = 0; // Reset Id to let EF Core handle it
                }

                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 544)
            {
                _logger.LogError(ex, "Failed to add product due to explicit IDENTITY_INSERT on Id.");
                throw new InvalidOperationException("Cannot set an explicit Id for a new product. The Id is auto-generated.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product: {Message}", ex.Message);
                throw; // Propagate the original exception
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                throw;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Product.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products.");
                throw;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await _context.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                throw;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "Product cannot be null.");

                var existingProduct = await _context.Product.FindAsync(product.Id);
                if (existingProduct == null)
                    throw new KeyNotFoundException($"Product with ID {product.Id} not found.");

                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {product.Id}.");
                throw;
            }
        }
    }
}