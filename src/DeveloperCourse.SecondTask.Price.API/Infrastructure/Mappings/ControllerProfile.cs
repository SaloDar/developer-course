using System.Collections.Generic;
using AutoMapper;
using DeveloperCourse.SecondTask.Price.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Price.API.DTOs;

namespace DeveloperCourse.SecondTask.Price.API.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<SecondTask.Price.Domain.Entities.Price, PriceDto>();

            CreateMap<IEnumerable<PriceDto>, GetPricesResponse>()
                .ForMember(x => x.Prices, x => x.MapFrom(t => t));
            
            CreateMap<PriceDto, CreatePriceResponse>()
                .ForMember(x => x.Price, x => x.MapFrom(t => t));   
            
            CreateMap<PriceDto, GetPriceResponse>()
                .ForMember(x => x.Price, x => x.MapFrom(t => t));       
            
            CreateMap<PriceDto, UpdatePriceResponse>()
                .ForMember(x => x.Price, x => x.MapFrom(t => t));
        }
    }
}