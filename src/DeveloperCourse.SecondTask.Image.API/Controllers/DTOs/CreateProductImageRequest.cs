using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class CreateProductImageRequest
    {
        [FromRoute(Name = "id")]
        public Guid ProductId { get; set; }

        [FromForm]
        public IFormFile Image { get; set; }
    }
}