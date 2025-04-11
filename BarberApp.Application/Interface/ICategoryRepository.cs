using BarberApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}