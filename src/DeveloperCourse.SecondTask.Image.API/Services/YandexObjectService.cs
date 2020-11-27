using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage;
using DeveloperCourse.SecondLesson.Common.Web.Exceptions;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Image.API.Services
{
    public class YandexObjectService : IDataStorageService
    {
        private readonly ILogger<YandexObjectService> _logger;
        
        private readonly YandexStorageService _yandexStorageService;
        
        public YandexObjectService(ILogger<YandexObjectService> logger, YandexStorageService yandexStorageService)
        {
            _logger = logger;
            _yandexStorageService = yandexStorageService;
        }

        public async Task<Uri> UploadFile(IFormFile file)
        {
            var fileName = GenerateFileName(file);
            
            var uploadedFileLink = await UploadFileToYandexObject(fileName,file.OpenReadStream());

            return uploadedFileLink;
        }

        private static string GenerateFileName(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file!.FileName);

            var fileExtension = Path.GetExtension(file!.FileName);

            var imageName = $"image_{fileName}_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss-fff}";

            using var md5 = MD5.Create();

            var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(imageName));

            return BitConverter.ToString(hashBytes) + fileExtension;
        }

        private async Task<Uri> UploadFileToYandexObject(string fileName, Stream fileStream)
        {
            try
            {
                var uploadedFile = await _yandexStorageService.PutObjectAsync(fileStream, fileName);

                if (!Uri.TryCreate(uploadedFile, UriKind.RelativeOrAbsolute, out var uploadedFileLink))
                {
                    throw new BadRequestException("Error when upload file on object storage.");
                }

                return uploadedFileLink;
            }
            catch (Exception e)
            {
                _logger.LogError("Yandex Disk API is unavailable", e);

                throw new ServiceUnavailableException("Yandex Disk API is unavailable", e);
            }
        }
    }
}