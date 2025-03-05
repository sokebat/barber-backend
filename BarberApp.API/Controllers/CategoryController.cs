using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ✅ Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();
                if (categories == null || !categories.Any())
                {
                    return NotFound(new { message = "No categories found." });
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving categories.",
                    error = ex.Message
                });
            }
        }

        // ✅ Get category by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }

            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the category.",
                    error = ex.Message
                });
            }
        }

        // ✅ Add a new category
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (category == null || string.IsNullOrWhiteSpace(category.Name))
            {
                return BadRequest(new { message = "Category name is required." });
            }

            try
            {
                await _categoryService.AddCategory(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the category.",
                    error = ex.Message
                });
            }
        }

        // ✅ Update a category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            if (id <= 0 || category == null || id != category.Id)
            {
                return BadRequest(new { message = "Invalid category data." });
            }

            try
            {
                await _categoryService.UpdateCategory(category);
                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the category.",
                    error = ex.Message
                });
            }
        }

        // ✅ Delete a category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }

            try
            {
                await _categoryService.DeleteCategory(id);
                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the category.",
                    error = ex.Message
                });
            }
        }
    }
}
