using System;
using Microsoft.AspNetCore.Http;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class CreateProductImageRequest
    {
        public Guid ProductId { get; set; }

        public IFormFile File { get; set; }
    }
}