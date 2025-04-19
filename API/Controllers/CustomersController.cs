using API.Extensions;
using Application.DTOs.Customers;
using Application.Validators;
using Canducci.Pagination;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Utils;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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
      private readonly IMapper Mapper;

      #region PrivateAction
      [NonAction]
      private async Task<Customer> GetByIdAsync(long id)
      {
         return await CustomerService.GetAsync(id);
      }  
      private CustomerResponse MapperToCustomerResponse(Customer customer)
      {
         return Mapper.Map<CustomerResponse>(customer);
      }
      #endregion

      public CustomersController(ICustomerService customerService, IUnitOfWork unitOfWork, IMapper mapper)
      {
         CustomerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
         UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
         Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      [HttpGet]
      [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Get()
      {
         List<Customer> items = await CustomerService.ToListAllAsync();
         List<CustomerResponse> data = Mapper.Map<List<CustomerResponse>>(items);
         return Ok(ApiResponse<List<CustomerResponse>>.Ok(data));
      }

      [HttpGet("page")]
      [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Page([FromQuery] PageRequest pageRequest)
      {
         Expression<Func<Customer, CustomerResponse>> select = x => new CustomerResponse(x.Id, x.Name, x.DateOfBirth);
         Expression<Func<Customer, long>> orderBy = x => x.Id;
         PaginatedRest<CustomerResponse> data = await CustomerService.ToPagedListAsync(pageRequest, select, orderBy);         
         return Ok(ApiResponse<PaginatedRest<CustomerResponse>>.Ok(data));
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
         CustomerResponse data = MapperToCustomerResponse(result);
         return Ok(ApiResponse<CustomerResponse>.Ok(data));
      }

      [HttpPost]
      [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<IActionResult> Post([FromBody] AddCustomerRequest request)
      {
         if (ModelState.IsProblem())
         {
            return BadRequest(ModelState);
         }
         Customer customer = Mapper.Map<Customer>(request);
         await CustomerService.AddAsync(customer);
         if (await UnitOfWork.CommitAsync() == false)
         {
            return BadRequest(ApiResponse.BadRequest("Failed to add customer"));
         }
         CustomerResponse data = MapperToCustomerResponse(customer);
         return CreatedAtAction(nameof(Get), new { id = customer.Id }, ApiResponse<CustomerResponse>.Created(data));
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
         Customer customer = Mapper.Map<Customer>(request);
         await CustomerService.UpdateAsync(customer);
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