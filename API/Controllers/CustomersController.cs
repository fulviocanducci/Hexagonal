using API.Extensions;
using Application.DTOs.Customers;
using Application.Validators;
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
   public class CustomersController : ControllerBase
   {
      private readonly ICustomerService CustomerService;
      private readonly IUnitOfWork UnitOfWork;

      #region PrivateAction
      [NonAction]
      private async Task<Customer> GetByIdAsync(long id)
      {
         return await CustomerService.GetAsync(id);
      }

      [NonAction]
      private Customer SetFields(AddCustomerRequest request)
      {
         return new Customer(request.Name, request.DateOfBirth);
      }

      [NonAction]
      private void SetFields(Customer customer, UpdateCustomerRequest request)
      {
         customer.SetName(request.Name);
         customer.SetDateOfBirth(request.DateOfBirth);         
      }
      #endregion

      public CustomersController(ICustomerService customerService, IUnitOfWork unitOfWork)
      {
         CustomerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
         UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      }

      [HttpGet]
      [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public IActionResult Get()
      {
         IAsyncEnumerable<Customer> data = CustomerService.GetAllAsync();
         return Ok(ApiResponse<IAsyncEnumerable<Customer>>.Ok(data));
      }

      [HttpGet("{id}")]
      [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Get(long id)
      {
         Customer result = await GetByIdAsync(id);
         if (result == null)
         {
            return NotFound(ApiResponse.NotFound($"Customer {id} not found"));
         }
         return Ok(ApiResponse<Customer>.Ok(result));
      }

      [HttpPost]
      [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Post([FromBody] AddCustomerRequest request)
      {
         if (ModelState.IsProblem())
         {
            return BadRequest(ModelState);
         }
         Customer customer = SetFields(request);
         await CustomerService.AddAsync(customer);
         if (await UnitOfWork.CommitAsync() == false)
         {
            return BadRequest(ApiResponse.BadRequest("Failed to add customer"));
         }
         return CreatedAtAction(nameof(Get), new { id = customer.Id }, ApiResponse<Customer>.Created(customer));
      }

      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Put(long id, [FromBody] UpdateCustomerRequest request)
      {
         if (ModelState.IsProblem())
         {
            return BadRequest(ModelState);
         }
         Customer customer = await GetByIdAsync(request.Id);
         if (customer == null)
         {
            return NotFound(ApiResponse.NotFound($"Customer {id} not found"));
         }
         SetFields(customer, request);
         if (await UnitOfWork.CommitAsync() == false)
         {
            return BadRequest(ApiResponse.BadRequest("Failed to update customer"));
         }
         return NoContent();
      }

      [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(long id)
      {
         Customer customer = await GetByIdAsync(id);
         if (customer == null)
         {
            return NotFound(ApiResponse.NotFound($"Customer {id} not found"));
         }
         await CustomerService.DeleteAsync(customer);
         if (await UnitOfWork.CommitAsync() == false)
         {
            return BadRequest(ApiResponse.BadRequest("Failed to delete customer"));
         }
         return Ok(customer);
      }
   }
}