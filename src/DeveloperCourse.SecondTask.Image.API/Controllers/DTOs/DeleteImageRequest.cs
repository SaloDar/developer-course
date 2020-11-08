using System;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class DeleteImageRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}