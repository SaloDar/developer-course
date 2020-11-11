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
    }
}