using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Common.Web.Exceptions;
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

        private readonly IDataStorageService _dataStorageService;

        private readonly IUserContext _userContext;

        public ImageService(ILogger<ImageService> logger, IMapper mapper, IImageContext imageContext,
            IDataStorageService dataStorageService, IUserContext userContext)
        {
            _logger = logger;
            _mapper = mapper;
            _imageContext = imageContext;
            _dataStorageService = dataStorageService;
            _userContext = userContext;
        }

        public async Task<ImageDto> UpdateImage(Guid id, Guid? productId = null, IFormFile image = null)
        {
            var productImage = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            if (productImage == null)
            {
                throw new NotFoundException($"Image with id {id} was not found.");
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
                throw new NotFoundException($"Image with id {id} was not found.");
            }

            return _mapper.Map<ImageDto>(image);
        }

        public async Task<ImageDto> CreateImage(Guid productId, IFormFile image)
        {
            if (!_userContext.IsAuthenticated || _userContext?.Identity == null ||
                _userContext.Identity.UserId == Guid.Empty)
            {
                throw new UnauthorizedException("Is not authenticated.");
            }

            if (productId == Guid.Empty)
            {
                throw new BadRequestException("Product id can't be empty");
            }

            var uploadedLink = await UploadImage(image);

            var productImage = new Domain.Entities.Image(productId, uploadedLink, _userContext.Identity.UserId);

            await _imageContext.Images.AddAsync(productImage);

            await _imageContext.SaveChangesAsync();

            return _mapper.Map<ImageDto>(productImage);
        }

        public async Task DeleteImage(Guid id)
        {
            var image = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                throw new NotFoundException($"Image with id {id} was not found.");
            }

            _imageContext.Images.Remove(image);

            await _imageContext.SaveChangesAsync();
        }

        private async Task<Uri> UploadImage(IFormFile image)
        {
            var contentType = image.ContentType.Split('/');

            if (!contentType?.FirstOrDefault()?.Equals("image", StringComparison.InvariantCultureIgnoreCase) ?? false)
            {
                throw new UnsupportedMediaTypeException("Unsupported type");
            }

            var fileLink = await _dataStorageService.UploadFile(image);

            return fileLink;
        }
    }
}