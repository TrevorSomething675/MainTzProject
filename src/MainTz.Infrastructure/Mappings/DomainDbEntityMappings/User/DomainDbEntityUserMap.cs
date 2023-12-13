using MainTz.Application.Models.UserEntities;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.DomainDbEntityMappings.User
{
    /// <summary>
    /// Конфигурация маппинга для User и UserDto
    /// </summary>
    public class DomainDbEntityUserMap : Profile
    {
        public DomainDbEntityUserMap()
        {
            //CreateMap<UserDomainEntity, UserEntity>()
            //    .ForMember(dest => dest.Cars, opts => opts.Ignore())
            //    .ForMember(dest => dest.CarsId, opts => opts
            //        .MapFrom(src => src.CarsId))

            //    .ForMember(dest => dest.Roles, opts => opts.Ignore())
            //    .ForMember(dest => dest.RoleId, opts => opts
            //        .MapFrom(src => src.RolesId))

            //    .ForMember(dest => dest.Notifications, opts => opts.Ignore())
            //    .ForMember(dest => dest.NotificationId, opts => opts
            //        .MapFrom(src => src.NotificationsId)).ReverseMap();
        }
    }
}