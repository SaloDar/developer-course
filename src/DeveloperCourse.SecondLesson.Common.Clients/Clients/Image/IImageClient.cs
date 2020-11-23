using System;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Image.DTOs.Responses;
using Refit;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Image
{
    [Headers("Authorization: Bearer")]
    public interface IImageClient
    {
        [Multipart]
        [Post("/images")]
        Task<CreateImageResponse> CreateImage(string productId, StreamPart file);

        [Get("/images/{id}")]
        Task<GetImageReponse> GetImage(Guid id);

        [Get("/images")]
        Task<GetImagesResponse> GetImages();

        [Get("/images")]
        Task<GetImagesResponse> GetImages(Guid productId);

        [Multipart]
        [Patch("/images/{id}")]
        Task<UpdateImageReponse> UpdateImage(Guid id, string productId);

        [Multipart]
        [Patch("/images/{id}")]
        Task<UpdateImageReponse> UpdateImage(Guid id, StreamPart file);

        [Multipart]
        [Patch("/images/{id}")]
        Task<UpdateImageReponse> UpdateImage(Guid id, string productId, StreamPart file);

        [Delete("/images/{id}")]
        Task DeleteImage(Guid id);
    }
}