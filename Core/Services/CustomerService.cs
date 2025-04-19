using Canducci.Pagination;
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

      public Task DeleteAsync(Customer customer)
      {
         return _repository.DeleteAsync(customer);
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

      public Task<List<Customer>> ToListAllAsync()
      {
         return _repository.ToListAllAsync();
      }

      public Task<PaginatedRest<Customer>> ToPagedListAsync<TKey>(IPageRequest pageRequest, Expression<Func<Customer, TKey>> orderBy)
      {
         return _repository.ToPagedListAsync<TKey>(pageRequest, orderBy);
      }

      public Task<PaginatedRest<TSelect>> ToPagedListAsync<TSelect, TKey>(IPageRequest pageRequest, Expression<Func<Customer, TSelect>> select, Expression<Func<Customer, TKey>> orderBy)
      {
         return _repository.ToPagedListAsync<TSelect, TKey>(pageRequest, select, orderBy);
      }

      public Task UpdateAsync(Customer customer)
      {
         return _repository.UpdateAsync(customer);
      }
   }
}
