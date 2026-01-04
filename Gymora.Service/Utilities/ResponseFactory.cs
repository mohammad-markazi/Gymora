using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Utilities
{
    public static class ResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data, string message = null, ApiStatusCode statusCode = ApiStatusCode.Ok)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static ApiResponse Success(string message = "عملیات با موفقیت انجام شد",ApiStatusCode statusCode=ApiStatusCode.Ok)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static ApiResponse Fail(string message,List<string>? errors=null,ApiStatusCode statusCode=ApiStatusCode.ValidationError)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>(){message},
                StatusCode = statusCode
            };
        }
        public static ApiResponse<T?> Fail<T>(string message, List<string>? errors = null, ApiStatusCode statusCode=ApiStatusCode.ValidationError)
        {
            return new ApiResponse<T?>
            {
                Success = false,
                Data = default,
                Message = message,
                Errors = errors ?? new List<string>() { message },
                StatusCode = statusCode
            };
        }
    }
}
