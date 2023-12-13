using MainTz.Application.Models.CarEntities;
using MainTz.Web.ViewModels.CarViewModels;
using AutoMapper;

namespace MainTz.Infrastructure.Mappings.RequestDomainMappings.Car
{
    internal class ResponseDomainCarMap : Profile
    {
        public ResponseDomainCarMap()
        {
            CreateMap<CarDomainEntity, CarRequest>();
        }
    }
}