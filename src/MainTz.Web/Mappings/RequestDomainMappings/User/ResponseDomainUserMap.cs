using MainTz.Application.Models.UserEntities;
using MainTz.Web.ViewModels.UserViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.User
{
    public class ResponseDomainUserMap : Profile
    {
        public ResponseDomainUserMap()
        {
            CreateMap<UserDomainEntity, UserResponse>();
        }
    }
}