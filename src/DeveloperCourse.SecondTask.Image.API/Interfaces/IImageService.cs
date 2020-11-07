using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Shared.Interfaces;
using DeveloperCourse.SecondTask.Image.API.DTOs;

namespace DeveloperCourse.SecondTask.Image.API.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAllImages();

        Task<IEnumerable<ImageDto>> GetProductImages(Guid productId);
    }
}