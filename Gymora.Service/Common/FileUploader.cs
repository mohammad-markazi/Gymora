using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Gymora.Service.Common;

public class FileUploader:IFileUploader
{
    private readonly IWebHostEnvironment
           _webHostEnvironment;

    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> Upload(IFormFile file, params string[] paths)
    {

        var pathInput = String.Join('\\', paths);

        var directoryPath = Path.Join(_webHostEnvironment.WebRootPath, pathInput);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = DateTime.Now.ToFileTime() + "-" + file.FileName;

        var filePath = Path.Combine(directoryPath, fileName);

        await using var output = File.Create(filePath);
        await file.CopyToAsync(output);
        return $"{String.Join('/', paths)}/{fileName}";
    }

    public string? Upload(string base64Strings, params string[] paths)
    {
        if (!string.IsNullOrEmpty(base64Strings))
        {

            var pathInput = String.Join('/', paths);

            var directoryPath = Path.Join(_webHostEnvironment.WebRootPath, pathInput);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = DateTime.Now.ToFileTime().ToString() + ".jpeg";

            var filePath = Path.Combine(directoryPath, fileName);
            byte[] imageBytes = Convert.FromBase64String(base64Strings);

            System.IO.File.WriteAllBytes(filePath, imageBytes);

            return $"{String.Join('/', paths)}/{fileName}";
        }

        return $"{String.Join('/', paths)}/not-pictured.png";
    }

    public void RemoveFile(string pathFile)
    {
        if (!string.IsNullOrWhiteSpace(pathFile))
        {
            pathFile = pathFile.Replace("/", "\\");
            var pathFilePhysics = Path.Combine(_webHostEnvironment.WebRootPath, pathFile);
            if (File.Exists(pathFilePhysics))
                File.Delete(pathFilePhysics);
        }

    }
}