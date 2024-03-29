﻿using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTa.Database.Context;
using AutoMapper;
using MainTz.Application.Models;

namespace MainTz.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;
		public RoleRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}
		public async Task<Role> GetRoleByNameAsync(string roleName)
		{
			await using(var context = _dbContextFactory.CreateDbContext())
			{
				var roleEntity = await context.Roles.FirstOrDefaultAsync(role => role.Name == roleName);
				var role = _mapper.Map<Role>(roleEntity);
				return role;
			}
		}
	}
}