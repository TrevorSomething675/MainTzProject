﻿using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Repositories;
using MainTz.RestApi.BLL.Services;

namespace MainTz.RestApi
{
    public static class bootstrapper
    {
        public static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IClientService, ClientService>();

            return services;
        }


    }
}
