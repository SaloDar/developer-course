using System;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Image.API.Controllers.DTOs
{
    public class GetImagesRequest
    {
        /// <summary>
        /// Product identifier, for filter the images.
        /// </summary>
        /// <example>BAD0468C-9CA9-4179-A20D-1F9EEE74318B</example>
        [FromQuery(Name = "productId")]
        public Guid? ProductId { get; set; } = null;
    }
}