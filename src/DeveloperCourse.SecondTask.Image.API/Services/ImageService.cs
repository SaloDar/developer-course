using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Image.API.Clients;
using DeveloperCourse.SecondTask.Image.API.DTOs;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Configs;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using DeveloperCourse.SecondTask.Image.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Image.API.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;

        private readonly IMapper _mapper;

        private readonly IImageContext _imageContext;

        private readonly IYandexDiskClient _yandexDiskClient;

        private readonly YandexDiskConfig _yandexDiskConfig;

        private readonly IHttpClientFactory _httpClientFactory;

        public ImageService(ILogger<ImageService> logger, IMapper mapper, IImageContext imageContext,
            IYandexDiskClient yandexDiskClient, IOptions<YandexDiskConfig> yandexDiskConfig,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _imageContext = imageContext;
            _yandexDiskClient = yandexDiskClient;
            _yandexDiskConfig = yandexDiskConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ImageDto> UpdateImage(Guid id, Guid? productId = null, IFormFile image = null)
        {
            var productImage = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            if (productImage == null)
            {
                throw new Exception($"Image with id {id} was not found.");
            }

            if (productId != null && productId.Value != Guid.Empty)
            {
                productImage.ChangeProduct(productId.Value);
            }

            if (image != null)
            {
                var uploadedLink = await UploadImage(image);

                productImage.ChangeLink(uploadedLink);
            }

            await _imageContext.SaveChangesAsync();

            return _mapper.Map<ImageDto>(productImage);
        }

        public async Task<IEnumerable<ImageDto>> GetImages(Guid? productId)
        {
            var images = await _imageContext.Images
                .Where(x => !productId.HasValue || productId.Value == Guid.Empty || x.ProductId == productId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }

        public async Task<ImageDto> GetImage(Guid id)
        {
            var image = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                throw new Exception($"Image with id {id} was not found.");
            }

            return _mapper.Map<ImageDto>(image);
        }

        public async Task<ImageDto> CreateImage(Guid productId, IFormFile image)
        {
            if (productId == Guid.Empty)
            {
                throw new InvalidOperationException("Product id can't be empty");
            }

            var uploadedLink = await UploadImage(image);

            var productImage = new Domain.Entities.Image(productId, uploadedLink);

            await _imageContext.Images.AddAsync(productImage);

            await _imageContext.SaveChangesAsync();

            return _mapper.Map<ImageDto>(productImage);
        }

        public async Task DeleteImage(Guid id)
        {
            var image = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                throw new Exception($"Image with id {id} was not found.");
            }

            _imageContext.Images.Remove(image);

            await _imageContext.SaveChangesAsync();
        }

        private async Task<Uri> UploadImage(IFormFile image)
        {
            var contentType = image.ContentType.Split('/');

            if (!contentType?.FirstOrDefault()?.Equals("image", StringComparison.InvariantCultureIgnoreCase) ?? false)
            {
                throw new InvalidOperationException("Unsupported type");
            }

            var fileName = Path.GetFileNameWithoutExtension(image!.FileName);

            var fileExtension = Path.GetExtension(image!.FileName);

            var imageName = $"image_{fileName}_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss-fff}";

            using var md5 = MD5.Create();

            var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(imageName));

            var newImageName = BitConverter.ToString(hashBytes) + fileExtension;

            var fileLink = await UploadFileToYandexDisk(newImageName, image.OpenReadStream());

            return fileLink;
        }

        private async Task<Uri> UploadFileToYandexDisk(string fileName, Stream fileStream)
        {
            var fileNameWithPath = Path.Combine(_yandexDiskConfig.BasePath, fileName);

            try
            {
                var uploadLink = await _yandexDiskClient.GetResourceUpload(fileNameWithPath);

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Authorization = _yandexDiskConfig.AuthenticationHeader;

                var uploadFileRequest = new HttpRequestMessage(new HttpMethod(uploadLink.Method), uploadLink.Href)
                {
                    Content = new StreamContent(fileStream)
                };

                await client.SendAsync(uploadFileRequest);

                var fileLink = await _yandexDiskClient.GetResourceDownload(fileNameWithPath);

                return fileLink.Href;
            }
            catch (Exception e)
            {
                _logger.LogError("Yandex Disk API is unavailable", e);

                throw new InvalidOperationException("Yandex Disk API is unavailable", e);
            }
        }
    }
}