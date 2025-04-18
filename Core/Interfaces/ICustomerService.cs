using Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
namespace Core.Interfaces
{
   public interface ICustomerService
   {
      Task AddAsync(Customer customer);
      Task UpdateAsync(Customer customer);
      Task DeleteAsync(long id);
      Task DeleteAsync(Customer customer);
      Task<Customer> GetAsync(long id);
      Task<Customer> GetAsync(Expression<Func<Customer, bool>> where);
      IAsyncEnumerable<Customer> GetAllAsync();
   }
}
