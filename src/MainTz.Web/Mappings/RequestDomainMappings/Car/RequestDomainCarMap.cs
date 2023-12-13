using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.Car
{
    public class RequestDomainCarMap : Profile
    {
        public RequestDomainCarMap()
        {
            CreateMap<UserResponse, UserDomainEntity>();

            CreateMap<UserDomainEntity, UserResponse>();
        }
    }
}