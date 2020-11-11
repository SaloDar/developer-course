using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Product.API.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondTask.Product.API.Clients
{
    public interface IImageClient
    {
        [Get("/images")]
        Task<GetImagesDto> GetImages(Guid? productId = null);
        
        [Get("/images/{id}")]
        Task<GetImageDto> GetPrice(Guid id);
    }
}