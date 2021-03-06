using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Image.API.Clients.DTOs;
using Refit;

namespace DeveloperCourse.SecondTask.Image.API.Clients
{
    public interface IYandexDiskClient
    {
        [Get("/v1/disk/resources/upload")]
        public Task<ResourceLinkDto> GetResourceUpload([AliasAs("path")] string filePath);

        [Get("/v1/disk/resources/download")]
        public Task<ResourceLinkDto> GetResourceDownload([AliasAs("path")] string filePath);

        [Put("/v1/disk/resources/publish")]
        public Task<ResourceLinkDto> PublishFile([AliasAs("path")] string filePath);

        [Get("/v1/disk/resources/public?fields=items")]
        public Task<PublicResourcesDto> GetPublicResources(
            [AliasAs("preview_crop")] bool previewCrop = false,
            [AliasAs("preview_size")] string previewSize = "1024x1024");

        [Get("/v1/disk/resources/last-uploaded?fields=items")]
        public Task<PublicResourcesDto> GetLastUpdatedResources(
            [AliasAs("preview_crop")] long limit,
            [AliasAs("media_type")] string mediaType,
            [AliasAs("preview_crop")] bool previewCrop = false,
            [AliasAs("preview_size")] string previewSize = "1024x1024");
    }
}