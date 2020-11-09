using System;
using System.Collections.Generic;
using DeveloperCourse.ThirdLesson.View.Services.Image.DTOs;
using DeveloperCourse.ThirdLesson.View.Services.Price.DTOs;

namespace DeveloperCourse.ThirdLesson.View.Services.Product.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Sku { get; set; }

        public string Weight { get; set; }

        public IEnumerable<ImageDto> Images { get; set; }

        public IEnumerable<PriceDto> Prices { get; set; }
    }
}