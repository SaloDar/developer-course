using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondTask.Product.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Product.API.DTOs;

namespace DeveloperCourse.SecondTask.Product.API.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<SecondTask.Product.Domain.Entities.Product, ProductDto>();

            CreateMap<IEnumerable<ProductDto>, GetProductsResponse>()
                .ForMember(x => x.Products, x => x.MapFrom(t => t));

            CreateMap<ProductDto, GetProductResponse>()
                .ForMember(x => x.Product, x => x.MapFrom(t => t));  
            
            CreateMap<ProductDto, CreateProductResponse>()
                .ForMember(x => x.Product, x => x.MapFrom(t => t)); 
            
            CreateMap<ProductDto, UpdateProductResponse>()
                .ForMember(x => x.Product, x => x.MapFrom(t => t));
        }
    }
}