using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public sealed class UnitOfWork : IUnitOfWork
   {
      private readonly AppDbContext _context;

      public UnitOfWork(AppDbContext context)
      {
         _context = context;
      }

      public int Commit()
      {
         return _context.SaveChanges();
      }

      public Task<int> CommitAsync()
      {
         return _context.SaveChangesAsync();
      }
   }
}
