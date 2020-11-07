using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IImageService
    {
        [Get("/image")]
        Task<GetAllImagesDto> GetAllImages();

        [Get("/image/product/{productId}")]
        Task<GetProductImages> GetProductImages(Guid productId);
    }
}