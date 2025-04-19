using Core.Interfaces;
namespace Infrastructure.Utils
{
   public class PageRequest : IPageRequest
   {
      public int Current { get; set; }
      public int Total { get; set; }
   }
}
