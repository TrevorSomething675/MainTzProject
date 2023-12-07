﻿using MainTz.RestApi.DAL.Repositories.Abstractions;
using MainTz.RestApi.BLL.Services.Abstractions;
using MainTz.RestApi.DAL.Repositories;
using MainTz.RestApi.BLL.Services;

namespace MainTz.RestApi
{
    /// <summary>
    /// Конфигурация сервисов и репозиториев
    /// </summary>
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
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
			services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }


        public static void UseAppServices(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseRouting();
        }
    }
}
