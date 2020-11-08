using System;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class GetImagesRequest
    {
        [FromQuery(Name = "productId")]
        public Guid ProductId { get; set; }
    }
}