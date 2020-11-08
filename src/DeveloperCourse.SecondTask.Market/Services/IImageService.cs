using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.DTOs;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services
{
    public interface IImageService
    {
        [Get("/images")]
        Task<GetImagesDto> GetImages(Guid? productId = null);
        
        [Get("/images/{id}")]
        Task<GetImageDto> GetPrice(Guid id);
    }
}