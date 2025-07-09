using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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






    }
}
