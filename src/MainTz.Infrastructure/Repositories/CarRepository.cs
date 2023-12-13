using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;
using MainTa.Database.Context;

namespace MainTz.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий CarRepository это обёртка на MainContext для таблицы Car
    /// </summary>
    public class CarRepository : ICarRepository
    {
        private readonly MainContext _context;
        public CarRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<CarEntity> GetCarById(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);
            return car;
        }

        public async Task<List<CarEntity>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
        }

        public async Task Create(CarEntity car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CarEntity car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CarEntity car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}