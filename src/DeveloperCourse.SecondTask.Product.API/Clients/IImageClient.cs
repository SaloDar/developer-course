using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondTask.Product.API.Clients
{
    public interface IImageClient
    {
        [Get("/image")]
        Task<GetAllImagesDto> GetAllImages();

        [Get("/image/product/{productId}")]
        Task<GetProductImages> GetProductImages(Guid productId);
    }
}