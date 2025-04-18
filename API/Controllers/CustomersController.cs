using Application.DTOs.Customers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CustomersController : ControllerBase
   {
      private readonly ICustomerService _customerService;
      private readonly IUnitOfWork _unitOfWork;

      public CustomersController(ICustomerService customerService, IUnitOfWork unitOfWork)
      {
         _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
         _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      // GET: api/<CustomersController>
      [HttpGet]
      public IAsyncEnumerable<Customer> Get()
      {
         return _customerService.GetAllAsync();
      }

      // GET api/<CustomersController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         Customer result = await _customerService.GetAsync(id);
         if (result == null)
         {
            return BadRequest("Customer not found");
         }
         return Ok(result);
      }

      // POST api/<CustomersController>
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] AddCustomerRequest request)
      {         
         if (ModelState.IsValid == false)
         {
            return BadRequest(ModelState);
         }
         Customer customer = new Customer
         {
            Name = request.Name,
            DateOfBirth = request.DateOfBirth
         };
         await _customerService.AddAsync(customer);
         if (await _unitOfWork.CommitAsync() <= 0)
         {
            return BadRequest("Failed to add customer");
         }
         return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
      }

      // PUT api/<CustomersController>/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put([FromBody] UpdateCustomerRequest request)
      {         
         if (ModelState.IsValid == false)
         {
            return BadRequest(ModelState);
         }
         Customer customer = await _customerService.GetAsync(request.Id);
         if (customer == null)
         {
            return NotFound("Customer not found");
         }
         customer.Name = request.Name; 
         customer.DateOfBirth = request.DateOfBirth;
         if (await _unitOfWork.CommitAsync() <= 0)
         {
            return BadRequest("Failed to update customer");
         }
         return NoContent();
      }

      // DELETE api/<CustomersController>/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
   }
}
