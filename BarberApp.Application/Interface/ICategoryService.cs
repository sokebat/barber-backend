

using BarberApp.Domain;

namespace BarberApp.Application.Interface
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task AddCategory(Category category);
        public Task DeleteCategory(int id);
        public Task UpdateCategory(Category category);
    }
}
