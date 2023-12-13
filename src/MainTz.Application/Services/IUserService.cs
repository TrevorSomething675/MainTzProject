using MainTz.Application.Models.UserEntities;

namespace MainTz.Application.Services
{
    public interface IUserService
    {
        public Task<UserDomainEntity> GetUserByNameAsync(string name);
        public Task<List<UserDomainEntity>> GetUsersAsync();
        public Task<bool> CreateAsync(UserDomainEntity userDto);
        public Task<bool> DeleteAsync(UserDomainEntity userDto);
        public Task<bool> UpdateAsync(UserDomainEntity userDto);
    }
}
