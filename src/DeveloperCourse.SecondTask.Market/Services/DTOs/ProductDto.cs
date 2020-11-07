using System;
using System.Collections.Generic;

namespace DeveloperCourse.ThirdLesson.View.Services.DTOs
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