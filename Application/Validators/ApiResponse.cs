using Microsoft.AspNetCore.Http;
namespace Application.Validators
{
   public class ApiResponse
   {
      public string Message { get; set; }
      public bool Success { get; set; }
      public int StatusCode { get; set; }

      #region Constructors
      public ApiResponse() { }

      public ApiResponse(string message)
      {
         Message = message;
      }

      public ApiResponse(bool success, int statusCode)
      {
         Success = success;
         StatusCode = statusCode;
      }

      public ApiResponse(bool success, int statusCode, string message)
      {
         Success = success;
         StatusCode = statusCode;
         Message = message;
      }
      #endregion Constructors

      public static ApiResponse NotFound(string message)
      {
         return new ApiResponse(false, StatusCodes.Status404NotFound, message);
      }

      public static ApiResponse BadRequest(string message)
      {
         return new ApiResponse(false, StatusCodes.Status400BadRequest, message);
      }
   }

   public class ApiResponse<T> : ApiResponse
   {
      public T Data { get; set; }

      #region Constructors
      public ApiResponse() { }
      #endregion

      public static ApiResponse<T> Ok(T data, string message = null)
      {
         return new ApiResponse<T>
         {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = StatusCodes.Status200OK
         };
      }

      public static ApiResponse<T> Created(T data, string message = null)
      {
         return new ApiResponse<T>
         {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = StatusCodes.Status201Created
         };
      }
   }
}