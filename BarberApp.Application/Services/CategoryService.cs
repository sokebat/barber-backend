using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await _categoryRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all categories.");
                throw;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid category ID provided.");
                    throw new ArgumentException("Category ID must be greater than zero.", nameof(id));
                }
                return await _categoryRepository.GetCategoryById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving category with ID {id}.");
                throw;
            }
        }

        public async Task AddCategory(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category), "Category cannot be null.");
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    _logger.LogWarning("Category name is required.");
                    throw new ArgumentException("Category name is required.", nameof(category.Name));
                }

                // Reset Id to ensure the database generates it
                category.Id = 0;
                await _categoryRepository.AddCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new category.");
                throw;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category), "Category cannot be null.");
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    _logger.LogWarning("Category name is required.");
                    throw new ArgumentException("Category name is required.", nameof(category.Name));
                }

                await _categoryRepository.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID {category.Id}.");
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid category ID provided.");
                    throw new ArgumentException("Category ID must be greater than zero.", nameof(id));
                }
                await _categoryRepository.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID {id}.");
                throw;
            }
        }
    }
}