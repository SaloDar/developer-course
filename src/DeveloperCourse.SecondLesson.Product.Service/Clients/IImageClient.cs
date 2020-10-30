using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Product.Service.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondLesson.Product.Service.Clients
{
    public interface IImageClient
    {
        [Get("/image")]
        Task<GetAllImagesDto> GetAllImages();

        [Get("/image/product/{productId}")]
        Task<GetProductImages> GetProductImages(Guid productId);
    }
}