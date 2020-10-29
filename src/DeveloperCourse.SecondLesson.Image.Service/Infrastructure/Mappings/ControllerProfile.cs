using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Image.Service.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<Entities.Image, ImageDto>();

            CreateMap<IEnumerable<ImageDto>, GetAllImagesResponse>()
                .ForMember(x => x.Images, x => x.MapFrom(t => t));

            CreateMap<IEnumerable<ImageDto>, GetProductImagesResponse>()
                .ForMember(x => x.Images, x => x.MapFrom(t => t));
        }
    }
}