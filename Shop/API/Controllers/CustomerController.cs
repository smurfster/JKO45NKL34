using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateCustomerRequestModel customer)
        {
            var result = await customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]int id, CreateUpdateCustomerRequestModel customer)
        {
            if (await customerService.Update(id, customer))
            {
                return NoContent();
            }


            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
