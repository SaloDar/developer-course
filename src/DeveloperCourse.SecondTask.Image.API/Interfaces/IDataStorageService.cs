using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Image.API.Interfaces
{
    public interface IDataStorageService
    {
        public Task<Uri> UploadFile(IFormFile file);
    }
}