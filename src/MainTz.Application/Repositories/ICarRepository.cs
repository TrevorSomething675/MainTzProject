using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface ICarRepository
    {
        public Task<CarEntity> GetCarById(int id);
        public Task<List<CarEntity>> GetCars();
        public Task Create(CarEntity car);
        public Task Delete(CarEntity car);
        public Task Update(CarEntity car);
    }
}