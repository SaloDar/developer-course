using System;
using System.Threading.Tasks;
using DeveloperCourse.ThirdLesson.View.Services.Image.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Refit;

namespace DeveloperCourse.ThirdLesson.View.Services.Image
{
    public interface IImageService
    {
        [Multipart]
        [Post("/images")]
        Task<CreateImageResponse> CreateImage(Guid id, Guid productId, IFormFile file);

        [Get("/images/{id}")]
        Task<GetImageReponse> GetImage(Guid id);

        [Get("/images")]
        Task<GetImagesResponse> GetImages(Guid? productId = null);

        [Multipart]
        [Patch("/images/{id}")]
        Task<UpdateImageReponse> UpdateImage(Guid id, Guid? productId = null, IFormFile file = null);

        [Delete("/images/{id}")]
        Task DeleteImage(Guid id);
    }
}