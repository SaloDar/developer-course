using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Image.API.DTOs;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Image.API.Interfaces
{
    public interface IImageService
    {
        Task<ImageDto> GetImage(Guid id);

        Task<ImageDto> UpdateImage(Guid id, Guid? productId = null, IFormFile image = null);

        Task<IEnumerable<ImageDto>> GetImages(Guid? productId = null);

        Task<ImageDto> CreateImage(Guid productId, IFormFile image);

        Task DeleteImage(Guid id);
    }
}