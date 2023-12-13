using MainTz.Application.Models.CarEntities;
using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using AutoMapper;

namespace MainTz.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarService> _logger;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, ILogger<CarService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _carRepository = carRepository;
        }
        public async Task<List<CarDomainEntity>> GetCars()
        {
            var carsEntity = await _carRepository.GetCars();
            var carsDomainEntity = _mapper.Map<List<CarDomainEntity>>(carsEntity);
            return carsDomainEntity;
        }
        public async Task<bool> CreateCar(CarDomainEntity carDomainEntity)
        {
            try
            {
                var carEntity = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.Create(carEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteCar(CarDomainEntity carDomainEntity)
        {
            try
            {
                var carEntity = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.Delete(carEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateCar(CarDomainEntity carDomainEntity)
        {
            try
            {
                var car = _mapper.Map<CarEntity>(carDomainEntity);
                await _carRepository.Update(car);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
    }
}
