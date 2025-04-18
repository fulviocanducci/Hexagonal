using Microsoft.AspNetCore.Http;

namespace Application.Validators
{
   public class ApiResponse<T>
   {
      public bool Success { get; set; }
      public string Message { get; set; } = string.Empty;
      public T Data { get; set; }
      public List<string> Errors { get; set; } = new List<string>();
      public int StatusCode { get; set; }
      public DateTime Timestamp { get; set; } = DateTime.UtcNow;

      public ApiResponse() { }

      public static ApiResponse<T> Ok(T data, string message = null)
      {
         return new ApiResponse<T>
         {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = 200
         };
      }

      public static ApiResponse<T> Ok(T data, string message = null, int statusCode = 200)
      {
         return new ApiResponse<T>
         {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = statusCode
         };
      }

      public static ApiResponse<T> Ok(string message = null)
      {
         return new ApiResponse<T>
         {
            Success = true,
            Data = default,
            Message = message,
            StatusCode = 200
         };
      }

      public static ApiResponse<T> Fail(string message, int statusCode = 400)
      {
         return new ApiResponse<T>
         {
            Success = false,
            Message = message,
            StatusCode = statusCode
         };
      }

      public static ApiResponse<T> Fail(List<string> errors, int statusCode = 400)
      {
         return new ApiResponse<T>
         {
            Success = false,
            Errors = errors,
            StatusCode = statusCode
         };
      }
   }
}