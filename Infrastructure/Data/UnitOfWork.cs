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

      public bool Commit()
      {
         return _context.SaveChanges() > 0;
      }

      public async Task<bool> CommitAsync()
      {
         return (await _context.SaveChangesAsync()) > 0; 
      }
   }
}
