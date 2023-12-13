﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using MainTz.Infrastructure.Repositories;
using MainTz.Application.Repositories;
using Microsoft.IdentityModel.Tokens;
using MainTz.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Services;
using MainTz.Extensions.Models;
using MainTa.Database.Context;
using MainTz.Web.Middleware;
using MainTz.Web.Mappings;
using MainTz.Extensions;
using System.Text;
using MainTz.Database.Entities;

namespace MainTz.Web
{
    public class Startup
    {
		DataBaseSettings dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
        JwtAuthSettings authSettings = Settings.Load<JwtAuthSettings>("JwtAuthSettings");
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseNpgsql(dbSettings.ConnectionString);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();

            services.AddDomainAppAutoMapperConfiguration();
            services.AddControllersWithViews();
			services.AddHttpContextAccessor();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
            .AddJwtBearer(options =>
            {
            	options.RequireHttpsMetadata = true;
            	options.SaveToken = true;
            	options.TokenValidationParameters = new TokenValidationParameters
            	{
            		ValidateIssuer = true,
            		ValidIssuer = authSettings.Issuer,
            		ValidateAudience = true,
            		ValidAudience = authSettings.Audience,
            		ValidateLifetime = true,
            		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key)),
            		ValidateIssuerSigningKey = true,
            	};
            });

			services.AddAuthorization();
		}

        public void Configure(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
                {
                    #region UserTestData
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                    if (!context.Roles.Any())
                    {
                        var roles = new List<RoleEntity>
                        {
                            new RoleEntity { RoleName = "Admin" },
                            new RoleEntity { RoleName = "Manager" },
                            new RoleEntity { RoleName = "User" }
                        };
                        context.Roles.AddRange(roles);
                        context.SaveChanges();
                    }

                    if(!context.Users.Any())
                    {
                        var users = new List<UserEntity>
                        {
                            new UserEntity
                            {
                                Name = "Admin",
                                Email = "Admin@mail.ru",
                                Password = "123123123",
                                Role = context.Roles.Where(role => role.RoleName == "Admin").FirstOrDefault()
                            },
                            new UserEntity
                            {
                                Name = "Manager",
                                Email = "Manager@mail.ru",
                                Password = "123123123",
                                Role = context.Roles.Where(role => role.RoleName == "Manager").FirstOrDefault()
                            }
                        };

                        context.Users.AddRange(users);
                        context.SaveChanges();
                    }

                    #endregion

                    #region CarTestData
                    if (!context.Brands.Any())
                    {
                        var brand = new BrandEntity
                        {
                            Name = "brand1",
                            Models = new List<ModelEntity>
                            {
                                new ModelEntity {Name = "Model1" }
                            }
                        };
                        context.Brands.Add(brand);
                        context.SaveChanges();
                    }

                    if (!context.Cars.Any())
                    {
                        var cars = new List<CarEntity> {
                        new CarEntity{
                            Name = "TestName1",
                            Color = "red",
                            Images = new List<ImageEntity>
                            {
                                new ImageEntity
                                {
                                    Name = "pic1",
                                    File = new byte[5],
                                }
                            },
                            IsFavorite = true,
                            IsVisible = true,
                            Description = "Description1Description1Descripti" +
                            "on1Description1Description1Description1",
                            Model = context.Models.FirstOrDefault()

                            },
                            new CarEntity{
                            Name = "TestName2",
                            Color = "red",
                            Images = new List<ImageEntity>
                            {
                                new ImageEntity
                                {
                                    Name = "pic2",
                                    File = new byte[5],
                                }
                            },
                            IsFavorite = true,
                            IsVisible = true,
                            Description = "Description2Description2Description2Description" +
                            "2Description2Description2Description2",
                            Model = context.Models.FirstOrDefault()

                            }
                        };
                        context.Cars.AddRange(cars);
                        context.SaveChanges();
                    }
                    #endregion
                }
            }
            app.UseHttpsRedirection();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseRouting();
            app.UseMiddleware<JwtHeaderMiddleware>();
			app.UseMiddleware<JwtRefreshMiddleware>();
			app.UseMiddleware<LoggingMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
	            name: "default",
	            pattern: "{controller=Auth}/{action=Register}/{id?}");
			});
        }
    }
}