using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondTask.Image.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Image.API.DTOs;

namespace DeveloperCourse.SecondTask.Image.API.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<SecondLesson.Image.Domain.Entities.Image, ImageDto>();

            CreateMap<IEnumerable<ImageDto>, GetAllImagesResponse>()
                .ForMember(x => x.Images, x => x.MapFrom(t => t));

            CreateMap<IEnumerable<ImageDto>, GetProductImagesResponse>()
                .ForMember(x => x.Images, x => x.MapFrom(t => t)); 
            
            CreateMap<ImageDto, CreateProductImageResponse>()
                .ForMember(x => x.Image, x => x.MapFrom(t => t));
        }
    }
}