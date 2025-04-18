using API.Extensions;
using Application.DTOs.Customers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
namespace API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   [Produces(MediaTypeNames.Application.Json)]
   public class CustomersController(ICustomerService customerService, IUnitOfWork unitOfWork) : ControllerBase
   {
      private readonly ICustomerService _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
      private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

      [HttpGet]
      [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public IAsyncEnumerable<Customer> Get()
      {
         return _customerService.GetAllAsync();
      }

      [HttpGet("{id}")]
      [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Get(int id)
      {
         Customer result = await _customerService.GetAsync(id);
         if (result == null)
         {
            return NotFound($"Customer {id} not found");
         }         
         return Ok(result.ToResponse());
      }

      [HttpPost]
      [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Post([FromBody] AddCustomerRequest request)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }
         Customer customer = request.ToModel();
         await _customerService.AddAsync(customer);
         if (await _unitOfWork.CommitAsync() <= 0)
         {
            return BadRequest("Failed to add customer");
         }
         return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
      }


      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
         request.ToModel(customer);
         if (await _unitOfWork.CommitAsync() <= 0)
         {
            return BadRequest("Failed to update customer");
         }
         return NoContent();
      }

      [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         Customer customer = await _customerService.GetAsync(id);
         if (customer == null)
         {
            return NotFound($"Customer {id} not found");
         }
         await _customerService.DeleteAsync(customer);
         if (await _unitOfWork.CommitAsync() <= 0)
         {
            return BadRequest("Failed to delete customer");
         }
         return Ok(new { message = "deleted", customer });
      }
   }
}
