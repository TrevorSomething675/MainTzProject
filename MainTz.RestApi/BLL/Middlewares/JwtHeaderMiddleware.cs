﻿namespace MainTz.RestApi.BLL.Middlewares
{
    /// <summary>
    /// Этот middleware добавляет токен в заголовок при каждом запросе
    /// </summary>
    public class JwtHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Cookies["accessToken"];
            if (token != "")
                context.Request.Headers.Append("Authorization", $"Bearer {token}");

            await _next(context);
        }
    }
}