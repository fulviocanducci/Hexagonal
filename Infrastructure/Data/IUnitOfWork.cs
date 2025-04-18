using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public interface IUnitOfWork
   {
      int Commit();
      Task<int> CommitAsync();
   }
}