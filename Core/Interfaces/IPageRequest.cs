namespace Core.Interfaces
{
   public interface IPageRequest
   {
      int Current { get; set; }
      int Total { get; set; }
   }
}
