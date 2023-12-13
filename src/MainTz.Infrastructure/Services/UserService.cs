using MainTz.Application.Repositories;
using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using MainTz.Database.Entities;
using AutoMapper;
using MainTz.Application.Models.UserEntities;

namespace MainTz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserDomainEntity> GetUserByNameAsync(string name)
        {
            var userEntity = await _userRepository.GetUserByName(name);
            var userDomainEntity = _mapper.Map<UserDomainEntity>(userEntity);
            return userDomainEntity;
        }
        public async Task<List<UserDomainEntity>> GetUsersAsync()
        {
            var usersEntity = await _userRepository.GetUsers();
            var usersDomainEntity = _mapper.Map<List<UserDomainEntity>>(usersEntity);
            return usersDomainEntity;
        }
        public async Task<bool> UpdateAsync(UserDomainEntity userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> CreateAsync(UserDomainEntity userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Create(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(UserDomainEntity userDto)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                await _userRepository.Delete(user);
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
