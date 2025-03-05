using BarberApp.Application.Interface;
using BarberApp.Domain;
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
                    return NotFound(new { message = "No products found." });  // 404 - Not Found
                }
                return Ok(products);  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving products.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Get a product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." });  // 400 - Bad Request
            }
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });  // 404 - Not Found
                }
                return Ok(product);  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the product.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Add a new product
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Product data is required." });  // 400 - Bad Request
            }
            try
            {
                await _productService.AddProduct(product);
                return Ok(new { message = "Product added successfully." });  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the product.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }

        // ✅ Update an existing product
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product == null || product.Id <= 0)
            {
                return BadRequest(new { message = "Invalid product data." });  // 400 - Bad Request
            }
            try
            {
                await _productService.UpdateProduct(product);
                return Ok(new { message = "Product updated successfully." });  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the product.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }


        // ✅ Delete an existing product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid product ID." });  // 400 - Bad Request
            }
            try
            {
                await _productService.DeleteProduct(id);
                return Ok(new { message = "Product deleted successfully." });  // 200 - OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the product.",
                    error = ex.Message
                });  // 500 - Internal Server Error
            }
        }
    }
}
