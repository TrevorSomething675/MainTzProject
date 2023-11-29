using MainTz.AuthApi.Services.Abstractions;
using Extensions.SettingsModels;
using MainTz.AuthApi.Services;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MainTz.RestApi.BLL.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var jwtAuthSettings = Settings.Load<AuthSettings>("JwtAuthSettings");

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtAuthSettings.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtAuthSettings.Audience,
			ValidateLifetime = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthSettings.Key)),
			ValidateIssuerSigningKey = true,
		};
	});
var app = builder.Build();

app.UseMiddleware<JwtHeaderMiddleware>();

app.Map("/GetTokens", async (context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string role = await reader.ReadToEndAsync();
    var tokenService = app.Services.GetRequiredService<ITokenService>();

    var accessToken = tokenService.CreateAccessToken(role);
    var refreshToken = tokenService.CreateRefreshToken(role);

    await context.Response.WriteAsJsonAsync(new { accessToken, refreshToken, role });
});

app.Map("/GetTokensOnRefresh", [Authorize] async (context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string role = await reader.ReadToEndAsync();
    var tokenService = app.Services.GetRequiredService<ITokenService>();

    var accessToken = tokenService.CreateAccessToken(role);
    var refreshToken = tokenService.CreateRefreshToken(role);

    await context.Response.WriteAsJsonAsync(new { accessToken, refreshToken, role });
});

app.Run();