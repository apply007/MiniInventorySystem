using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using MiniInventorySystem.Interfaces;
using MiniInventorySystem.Model;

namespace MiniInventorySystem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _repo.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var customer = await _repo.GetByIdAsync(id);
                if (customer == null) return NotFound();
                return Ok(customer);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var created = await _repo.AddAsync(customer);
                    return CreatedAtAction(nameof(GetById), new { id = created.CustomerId }, created);
                }
                return NotFound();


            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPut("EditCustomer/{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            try
            {
                var updated = await _repo.UpdateAsync(id, customer);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
