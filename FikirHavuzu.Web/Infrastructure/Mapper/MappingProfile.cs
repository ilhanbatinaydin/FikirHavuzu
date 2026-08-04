using AutoMapper;
using FikirHavuzu.Entity.Dtos.Auth;
using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Dtos.User;
using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Web.Models;

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

            CreateMap<Idea, IdeaDetailDto>()
                .ForMember(dest => dest.AddedByUserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.AddedByUserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<IdeaCreateDto, Idea>();

            CreateMap<IdeaCreateViewModel, IdeaCreateDto>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber ?? "-"))
                .ForMember(dest => dest.Permissions, opt =>
                    opt.MapFrom(src => src.UserPermissions.Select(up => up.Permission.Name).ToList()));

            CreateMap<UserCreateDto, User>();

            CreateMap<UserCreateViewModel, UserCreateDto>();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserUpdateDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());

            CreateMap<UserUpdateDto, UserUpdateViewModel>().ReverseMap();

            CreateMap<Permission, PermissionWithDependenciesDto>()
                .ForMember(dest => dest.RequiredPermissionIds, opt => opt.MapFrom(src =>
                    src.RequiredPermissions.Select(dp => dp.RequiredPermissionId).ToList()
                ));

            CreateMap<User, UserPermissionAssignmentDto>();

            CreateMap<UserPermissionAssignmentDto, UserPermissionAssignmentViewModel>();

            CreateMap<Evaluation, EvaluationDto>()
                .ForMember(dest => dest.EvaluatorFullName, opt => opt.MapFrom(src => src.EvaluatedByUser.FirstName + " " + src.EvaluatedByUser.LastName))
                .ForMember(dest => dest.EvaluatorEmail, opt => opt.MapFrom(src => src.EvaluatedByUser.Email));

            CreateMap<EvaluationCreateDto, Evaluation>();

            CreateMap<EvaluationCreateViewModel, EvaluationCreateDto>();

            CreateMap<IdeaDocument, IdeaDocumentDto>();

        }
    }
}
