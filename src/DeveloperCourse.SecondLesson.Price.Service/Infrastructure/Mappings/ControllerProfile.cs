using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondLesson.Price.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Price.Service.DTOs;

namespace DeveloperCourse.SecondLesson.Price.Service.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<Entities.Price, PriceDto>();

            CreateMap<IEnumerable<PriceDto>, GetAllPricesResponse>()
                .ForMember(x => x.Prices, x => x.MapFrom(t => t));

            CreateMap<IEnumerable<PriceDto>, GetProductPricesResponse>()
                .ForMember(x => x.Prices, x => x.MapFrom(t => t));
        }
    }
}