using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public interface IUnitOfWork
   {
      bool Commit();
      Task<bool> CommitAsync();
   }
}