using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberApp.Application.Interface;
using BarberApp.Domain;
using CacheManager.Core.Logging;
using Microsoft.Extensions.Logging;

namespace BarberApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _productRepository.GetAllProducts();
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
                return await _productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                throw;
            }
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                await _productRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new product.");
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                throw;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                await _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {product.Id}.");
                throw;
            }

        }


    }
}
