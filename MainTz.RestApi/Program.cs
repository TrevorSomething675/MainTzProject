using MainTz.RestApi.Configurations.AutoMapperConfiguration;
using MainTz.RestApi.Configurations.NLogConfiguration;
using MainTz.RestApi.Configurations.AuthConfigration;
using MainTz.RestApi.dal.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MainTz.RestApi.Configurations;
using Extensions.SettingsModels;
using MainTz.RestApi;
using Extensions;
using MainTz.RestApi.MiddleWares;
using MainTz.RestApi.Configurations.IdentityConfiguration;

var builder = WebApplication.CreateBuilder(args);

var dbSettings = Settings.Load<DataBaseSettings>("DataBaseSettings");
var jwtAuthSettings = Settings.Load<AuthSettings>("JwtAuthSettings");

var services = builder.Services;

services.AddAppLogger(); // ���������� �������
services.AddAppAutoMapperConfiguration(); // ������������ �����������
services.AddAppDbContext(dbSettings);
services.AddAppSwagger();

services.AddAppRepositories(); //����������� ������������
services.AddAppServices(); //����������� ��������
services.AddAppAuth(jwtAuthSettings); // ��������������
services.AddAppIdentity(); //��������� Identity

services.AddAuthorization();

var app = builder.Build();

#region testData
using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<MainContext>())
    {
        try
        {
            context.Database.Migrate();
        }
        catch
        {
            context.Database.EnsureCreated();
        }
    }
}
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseRouting();

app.UseAppAuth();
//app.UseMiddleware<JwtMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAppSwagger();

app.Run();