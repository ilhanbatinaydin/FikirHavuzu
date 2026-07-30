using AutoMapper;
using FikirHavuzu.Entity.Dtos.Auth;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Web.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginResponseDto>()
                .ForMember(dest => dest.Permissions, opt =>
                    opt.MapFrom(src => src.UserPermissions.Select(up => up.Permission.Name).ToList()));

            CreateMap<Category, CategoryDto>();
            CreateMap<Idea, IdeaDto>()
                .ForMember(dest => dest.AddedByUserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<IdeaCreateDto, Idea>();
        }
    }
}
