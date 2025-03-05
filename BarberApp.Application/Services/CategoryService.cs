using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.Extensions.Logging;

namespace BarberApp.Application.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;


        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
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
                await _categoryRepository.AddCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new category.");
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                await _categoryRepository.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID {id}.");
                throw;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                await _categoryRepository.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID {category.Id}.");
                throw;
            }
        }




    }
}
