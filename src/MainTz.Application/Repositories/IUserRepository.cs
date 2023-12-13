using MainTz.Database.Entities;

namespace MainTz.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserByName(string name);
        public Task<List<UserEntity>> GetUsers();
        public Task Update(UserEntity user);
        public Task Create(UserEntity user);
        public Task Delete(UserEntity user);
    }
}
