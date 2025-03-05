 
using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BarberApp.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly BarberDbContext _context;


        public ProductRepository(BarberDbContext context, ILogger<ProductRepository> logger) {

            _context = context;
            _logger = logger;
        }

      
       public async Task AddProduct(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "Product cannot be null.");
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new product.");
                throw new Exception("An error occurred while adding the product.");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = _context.Product.Find(id);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                throw new Exception("An error occurred while deleting the product.");
            }

        }

      public  async Task<List<Product>> GetAllProducts()
        {
            try
            {
               return await _context.Product.AsNoTracking().ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products.");
                throw new Exception("Database error occurred while retrieving products.");
            }
        }

       public  async Task<Product> GetProductById(int id)
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
                    throw new KeyNotFoundException("Product not found.");

                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await  _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product.");
                throw new Exception("An error occurred while updating the product.");
            }
        }
    }
}
