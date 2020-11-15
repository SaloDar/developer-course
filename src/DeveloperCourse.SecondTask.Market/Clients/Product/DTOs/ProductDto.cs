using System;
using System.Collections.Generic;
using DeveloperCourse.SecondTask.Market.Clients.Image.DTOs;
using DeveloperCourse.SecondTask.Market.Clients.Price.DTOs;

namespace DeveloperCourse.SecondTask.Market.Clients.Product.DTOs
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