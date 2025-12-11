using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Utilities
{
    public static class ResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data, string message = null, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }
        public static ApiResponse Success(string message = "عملیات با موفقیت انجام شد")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }
        public static ApiResponse Fail(string message,object? errors=null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = errors ?? message
            };
        }
        public static ApiResponse<T?> Fail<T>(string message, object errors = null, int statusCode = 400)
        {
            return new ApiResponse<T?>
            {
                Success = false,
                Data = default,
                Message = message,
                Errors = errors
            };
        }
    }
}
