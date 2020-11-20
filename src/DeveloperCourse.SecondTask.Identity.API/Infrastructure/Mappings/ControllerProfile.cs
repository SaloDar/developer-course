using AutoMapper;
using DeveloperCourse.SecondTask.Identity.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Identity.API.DTOs;

namespace DeveloperCourse.SecondTask.Identity.API.Infrastructure.Mappings
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<Domain.Entities.User, UserDto>();

            CreateMap<UserDto, GetCurrentUserResponse>()
                .ForMember(x => x.User, x => x.MapFrom(t => t));

            CreateMap<UserDto, CreateUserResponse>()
                .ForMember(x => x.User, x => x.MapFrom(t => t));

            CreateMap<string, AuthenticateUserResponse>()
                .ForMember(x => x.Token, x => x.MapFrom(t => t));
        }
    }
}