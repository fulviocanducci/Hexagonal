using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Infrastructure.Repositories
{
   public class CustomerRepository(AppDbContext context) : ICustomerRepository
   {
      private readonly AppDbContext _context = context;

      public async Task AddAsync(Customer customer)
      {
         await _context.Customers.AddAsync(customer);
      }

      public async Task DeleteAsync(long id)
      {
         var model = await GetAsync(id);
         if (model is not null)
         {
            _context.Customers.Remove(model);
         }
      }

      public IAsyncEnumerable<Customer> GetAllAsync()
      {
         return _context.Customers.AsNoTracking().AsAsyncEnumerable();
      }

      public async Task<Customer> GetAsync(long id)
      {
         return await _context.Customers.FindAsync(id);
      }

      public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> where)
      {
         return await _context.Customers.Where(where).FirstOrDefaultAsync();
      }

      public Task UpdateAsync(Customer customer)
      {
         _context.Customers.Update(customer);
         return Task.CompletedTask;
      }
   }
}
