using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondLesson.Product.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Product.Service.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<Entities.Product, ProductDto>();

            CreateMap<IEnumerable<ProductDto>, GetAllProductsResponse>()
                .ForMember(x => x.Products, x => x.MapFrom(t => t));

            CreateMap<ProductDto, GetProductResponse>()
                .ForMember(x => x.Product, x => x.MapFrom(t => t));
        }
    }
}