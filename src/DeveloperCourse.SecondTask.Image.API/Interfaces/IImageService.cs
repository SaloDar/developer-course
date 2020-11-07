using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Image.API.DTOs;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Image.API.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAllImages();

        Task<IEnumerable<ImageDto>> GetProductImages(Guid productId);
        
        Task<ImageDto> CreateProductImage(Guid productId, IFormFile image);
    }
}