using AutoMapper;
using FikirHavuzu.Entity.Dtos.Auth;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Dtos.User;
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

            CreateMap<Permission, PermissionDto>();

            CreateMap<Idea, IdeaDto>()
                .ForMember(dest => dest.AddedByUserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<IdeaCreateDto, Idea>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber ?? "-"))
                .ForMember(dest => dest.Permissions, opt =>
                    opt.MapFrom(src => src.UserPermissions.Select(up => up.Permission.Name).ToList()));
        }
    }
}
