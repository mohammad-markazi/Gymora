using Microsoft.AspNetCore.Http;

namespace Gymora.Service.Common
{
    public interface IFileUploader
    {
        Task<string> Upload(IFormFile file, params string[] paths);
        string? Upload(string base64Strings, params string[] paths);
        void RemoveFile(string pathFile);
    }
}
