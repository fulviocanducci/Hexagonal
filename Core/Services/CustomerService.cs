using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Core.Services
{
   public class CustomerService : ICustomerService
   {
      private readonly ICustomerRepository _repository;

      public CustomerService(ICustomerRepository repository)
      {
         _repository = repository;
      }

      public Task AddAsync(Customer customer)
      {
         return _repository.AddAsync(customer);
      }

      public Task DeleteAsync(long id)
      {
         return _repository.DeleteAsync(id);
      }

      public IAsyncEnumerable<Customer> GetAllAsync()
      {
         return _repository.GetAllAsync();
      }

      public Task<Customer> GetAsync(long id)
      {
         return _repository.GetAsync(id);
      }

      public Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
      {
         return _repository.GetAsync(where);
      }

      public Task UpdateAsync(Customer customer)
      {
         return _repository.UpdateAsync(customer);
      }
   }
}
