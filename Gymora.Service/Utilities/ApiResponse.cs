using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Utilities
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public ApiStatusCode StatusCode { get; set; }

    }

    public class ApiResponse<T>: ApiResponse
    {
        public T Data { get; set; }
    }

    public enum ApiStatusCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,

        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,

        Conflict = 409,
        ValidationError = 422,

        InternalServerError = 500
    }
}
