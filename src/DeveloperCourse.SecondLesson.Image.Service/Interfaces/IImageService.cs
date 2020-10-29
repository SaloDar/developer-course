using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Image.Service.Interfaces
{
    public interface IImageService
    {
        IEnumerable<ImageDto> GetAllImages();

        IEnumerable<ImageDto> GetProductImages(Guid productId);
    }
}