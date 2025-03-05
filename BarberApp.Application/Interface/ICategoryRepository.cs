using BarberApp.Domain;

namespace BarberApp.Application.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task DeleteCategory(int id);
        Task UpdateCategory(Category category);


    }
}
