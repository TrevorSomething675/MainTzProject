using MainTz.Application.Models.CarEntities;

namespace MainTz.Application.Services
{
    public interface ICarService
    {
        public Task<List<CarDomainEntity>> GetCars();
        public Task<bool> CreateCar(CarDomainEntity carDto);
        public Task<bool> DeleteCar(CarDomainEntity carDto);
        public Task<bool> UpdateCar(CarDomainEntity carDto);
    }
}