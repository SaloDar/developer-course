using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Domain.Interfaces;
using DeveloperCourse.SecondTask.Image.API.DTOs;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
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

        public async Task<IEnumerable<ImageDto>> GetImages(Guid? productId)
        {
            var images = await _imageContext.Images.Where(x =>  !productId.HasValue || productId.Value == Guid.Empty || x.ProductId == productId).ToListAsync();
            
            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }

        public async Task<ImageDto> GetImage(Guid id)
        {
            var images = await _imageContext.Images.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ImageDto>(images);
        }

        public async Task<ImageDto> CreateImage(Guid productId, IFormFile image)
        {
            if (productId == Guid.Empty)
            {
                throw new InvalidOperationException("GUID can't be empty");
            }
            
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

            var productImage = new SecondLesson.Image.Domain.Entities.Image(productId, uploadedLink);

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
    }
}