using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;
using DeveloperCourse.SecondLesson.Shared.Interfaces;

namespace DeveloperCourse.SecondLesson.Image.Service.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>> GetAllImages();

        Task<IEnumerable<ImageDto>> GetProductImages(Guid productId);
    }
}