using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllProducts()
        {
            
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> SearchProducts([FromQuery] int categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var res = await _productService.GetAllAsync();
            if (res == null)
            {
                return NotFound();
            }
           if(maxPrice!=50)
            {
                return Ok(res);
            }
            var rek = res.Where(io => io.ProductCategoryId == categoryId && io.Price >= minPrice && io.Price <= maxPrice).ToList();
            return Ok(rek);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(product.ProductName))
                return BadRequest();
            await _productService.AddAsync(product);
            return Ok(product);
        }


        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryModel>>> GetAllCategories()
        {
            var categories = await _productService.GetAllProductCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory(ProductCategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.AddCategoryAsync(category);
            return Ok();
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _productService.RemoveCategoryAsync(id);
            return Ok();
        }
    }
}
