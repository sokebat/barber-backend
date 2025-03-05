using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BarberApp.Persistence.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly BarberDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(BarberDbContext context , ILogger<CategoryRepository> logger) {
        
            _context = context;
            _logger = logger;
        }


        public async Task<List<Category>> GetAllCategories ()
        {
            try
            {
                return await _context.Category.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all categories.");
                throw new Exception("Database error occurred while retrieving categories.");
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var category = await _context.Category.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                return category;
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
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding a new category.");
                throw new Exception("An error occurred while adding the category.");
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var category = _context.Category.Find(id);
                if (category == null)
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID {id}.");
                throw new Exception("An error occurred while deleting the category.");
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category), "Category cannot be null.");

                var existingCategory = _context.Category.Find(category.Id);
                if (existingCategory == null)
                    throw new KeyNotFoundException($"Category with ID {category.Id} not found ");

                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID {category.Id}.");
                throw new Exception("An error occurred while updating the category.");
            }
        }
    }
}
