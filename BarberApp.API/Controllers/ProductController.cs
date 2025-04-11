using BarberApp.Application.Interface;
using BarberApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // ✅ Get all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                if (products == null || !products.Any())
                {
                    return NotFound(new { message = "No products found." }); // 404 - Not Found
                }

                return Ok(products); // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving products.",
                    error = ex.Message
                }); // 500 - Internal Server Error
            }
        }

        // ✅ Get a product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." }); // 400 - Bad Request
            }

            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." }); // 404 - Not Found
                }
                return Ok(product); // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occurred while retrieving the product with ID {id}.",
                    error = ex.Message
                }); // 500 - Internal Server Error
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Invalid product data." }); // 400 - Bad Request
            }

            if (string.IsNullOrWhiteSpace(product.Name) ||
                string.IsNullOrWhiteSpace(product.Description) ||
                string.IsNullOrWhiteSpace(product.ImageUrl) ||
                string.IsNullOrWhiteSpace(product.CategoryName) ||
                product.Price <= 0)
            {
                return BadRequest(new { message = "Product name, description, image URL, category, and valid price are required." }); // 400 - Bad Request
            }

            try
            {
                await _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product); // 201 - Created
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 - Bad Request
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the product.",
                    error = ex.InnerException?.Message ?? ex.Message
                }); // 500 - Internal Server Error
            }
        }

        // ✅ Update an existing product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." }); // 400 - Bad Request
            }

            if (product == null || id != product.Id)
            {
                return BadRequest(new { message = "Product data is invalid or ID mismatch." }); // 400 - Bad Request
            }

            try
            {
                await _productService.UpdateProduct(product);
                return NoContent(); // 204 - No Content (successful update)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the product.",
                    error = ex.InnerException?.Message ?? ex.Message
                }); // 500 - Internal Server Error
            }
        }

        // ✅ Delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." }); // 400 - Bad Request
            }

            try
            {
                await _productService.DeleteProduct(id);
                return NoContent(); // 204 - No Content (successful delete)
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404 - Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the product.",
                    error = ex.InnerException?.Message ?? ex.Message
                }); // 500 - Internal Server Error
            }
        }
    }
}