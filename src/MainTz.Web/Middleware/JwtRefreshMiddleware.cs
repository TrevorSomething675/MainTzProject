using System.IdentityModel.Tokens.Jwt;
using MainTz.Application.Services;
using MainTz.Extensions.Models;

namespace MainTz.Web.Middleware
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        public JwtRefreshMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Cookies["accessToken"]))
            {
                var accessToken = context.Request.Cookies["accessToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtTokenAccessToken = handler.ReadJwtToken(accessToken);
                var accessTokenValidTo = jwtTokenAccessToken.ValidTo;

                if (accessTokenValidTo > DateTime.Now)
                {
					var role = context.Request.Cookies["role"];
                    var newRefreshToken = _tokenService.CreateRefreshToken(role);

                    if (!string.IsNullOrEmpty(newRefreshToken))
                    {
                        var refreshTokenModel = new TokensModel { 
                            RefreshToken = newRefreshToken, 
                            AccessToken = _tokenService.CreateAccessToken(role),
                            Role = role 
                        };
                        context.Response.Cookies.Append("accessToken", refreshTokenModel.AccessToken);
                        context.Response.Cookies.Append("refreshToken", refreshTokenModel.RefreshToken);
                        context.Response.Cookies.Append("role", refreshTokenModel.Role);
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}
