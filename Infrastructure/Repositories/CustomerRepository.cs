﻿using Canducci.Pagination;
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

      public Task DeleteAsync(Customer customer)
      {
         _context.Customers.Remove(customer);
         return Task.CompletedTask;
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

      public Task<List<Customer>> ToListAllAsync()
      {
         return _context.Customers.AsNoTracking().ToListAsync();
      }

      public Task<PaginatedRest<Customer>> ToPagedListAsync<TKey>(IPageRequest pageRequest, Expression<Func<Customer, TKey>> orderBy)
      {
         return _context
            .Customers
            .AsNoTracking()
            .OrderBy(orderBy)
            .ToPaginatedRestAsync(pageRequest.Current, pageRequest.Total);
      }

      public Task<PaginatedRest<TSelect>> ToPagedListAsync<TSelect, TKey>(IPageRequest pageRequest, Expression<Func<Customer, TSelect>> select, Expression<Func<Customer, TKey>> orderBy)
      {
         return _context
            .Customers
            .AsNoTracking()
            .OrderBy(orderBy)
            .Select(select)
            .ToPaginatedRestAsync(pageRequest.Current, pageRequest.Total);
      }

      public Task UpdateAsync(Customer customer)
      {
         _context.Customers.Update(customer);
         return Task.CompletedTask;
      }
   }
}
