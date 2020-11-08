using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Image.API.DTOs;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using DeveloperCourse.SecondTask.Image.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondTask.Image.API.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;

        private readonly IImageContext _imageContext;
        
        public ImageService(IMapper mapper, IImageContext imageContext)
        {
            _mapper = mapper;
            _imageContext = imageContext;
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
            var images = await _imageContext.Images.Where(x =>  !productId.HasValue || productId.Value == Guid.Empty || x.ProductId == productId).ToListAsync();
            
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
            var product = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            _imageContext.Images.Remove(product);
            
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
            
            //todo:replace to Yandex Object Storage request.
            var uploadedLink = new Uri("http://localhost");

            return uploadedLink;
        }
    }
}