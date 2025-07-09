using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using MiniInventorySystem.Interfaces;
using MiniInventorySystem.Model;

namespace MiniInventorySystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase

    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetProducts(
    string? search = null,
    string? category = null,
    int page = 1,
    int pageSize = 10)
        {
            var (items, totalCount) = await _productRepository.GetFilteredProductsAsync(search, category, page, pageSize);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                items
            });
        }
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetAllProduct([FromRoute] int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null) return NotFound();
                return Ok(product);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productRepository.AddAsync(product);

                    return Ok("Product Added Successfully");
                }
                return BadRequest();



            }
            catch (Exception ex)
            {

                throw ex;
            }

           


        }
        [HttpPut("EditProduct/{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            try
            {
                var updated = await _productRepository.UpdateAsync(id, product);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {
               return BadRequest(ex);
            
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _productRepository.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
