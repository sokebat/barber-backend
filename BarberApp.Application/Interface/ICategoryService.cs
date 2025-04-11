using BarberApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category); // Keep using Category directly
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}