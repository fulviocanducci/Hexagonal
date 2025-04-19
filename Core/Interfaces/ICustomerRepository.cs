using Canducci.Pagination;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Core.Interfaces
{
   public interface ICustomerRepository
   {
      Task AddAsync(Customer customer);
      Task UpdateAsync(Customer customer);
      Task DeleteAsync(long id);
      Task DeleteAsync(Customer customer);
      Task<Customer> GetAsync(long id);
      Task<Customer> GetAsync(Expression<Func<Customer, bool>> where);
      IAsyncEnumerable<Customer> GetAllAsync();
      Task<List<Customer>> ToListAllAsync();
      Task<PaginatedRest<Customer>> ToPagedListAsync<TKey>(IPageRequest pageRequest, Expression<Func<Customer, TKey>> orderBy);
      Task<PaginatedRest<TSelect>> ToPagedListAsync<TSelect, TKey>(IPageRequest pageRequest, Expression<Func<Customer, TSelect>> select, Expression<Func<Customer, TKey>> orderBy);
   }
}
