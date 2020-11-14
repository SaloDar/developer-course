using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Image.API.Clients;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Image.API.Services
{
    public class YandexDiskService : IDataStorageService
    {
        private readonly ILogger<YandexDiskService> _logger;

        private readonly IYandexDiskClient _yandexDiskClient;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly YandexDiskConfig _yandexDiskConfig;

        public YandexDiskService(ILogger<YandexDiskService> logger, IYandexDiskClient yandexDiskClient,
            IHttpClientFactory httpClientFactory, IOptions<YandexDiskConfig> yandexDiskConfig)
        {
            _logger = logger;
            _yandexDiskClient = yandexDiskClient;
            _httpClientFactory = httpClientFactory;
            _yandexDiskConfig = yandexDiskConfig.Value;
        }

        public async Task<Uri> UploadFile(IFormFile file)
        {
            var fileName = GenerateFileName(file);

            var filePath = Path.Combine(_yandexDiskConfig.BasePath, fileName);

            await UploadFileToYandexDisk(filePath, file.OpenReadStream());

            await _yandexDiskClient.PublishFile(filePath);

            var publicFiles = await _yandexDiskClient.GetPublicResources();

            var uploadedFile = publicFiles.Items.FirstOrDefault(x =>
                x.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            if (uploadedFile == null)
            {
                throw new Exception($"File with name {fileName} not found on yandex disk.");
            }

            if (uploadedFile.PreviewLink == null)
            {
                throw new Exception($"Preview link for file with name {fileName} is empty.");
            }

            return uploadedFile.PreviewLink;
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

        private async Task UploadFileToYandexDisk(string filePath, Stream fileStream)
        {
            try
            {
                var uploadLink = await _yandexDiskClient.GetResourceUpload(filePath);

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = _yandexDiskConfig.AuthenticationHeader;

                var uploadFileRequest = new HttpRequestMessage(new HttpMethod(uploadLink.Method), uploadLink.Href)
                {
                    Content = new StreamContent(fileStream)
                };

                await client.SendAsync(uploadFileRequest);
            }
            catch (Exception e)
            {
                _logger.LogError("Yandex Disk API is unavailable", e);

                throw new InvalidOperationException("Yandex Disk API is unavailable", e);
            }
        }
    }
}