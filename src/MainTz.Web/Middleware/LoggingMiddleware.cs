using System.Text;

namespace MainTz.Web.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string bodyContent = "";

            using (var reader = new StreamReader(context.Request.Body))
            {
                bodyContent = await reader.ReadToEndAsync();
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyContent));
            }

            var requestUrl = context.Request.Path;
            var requestMethod = context.Request.Method;
            var requestHeaderAuth = context.Request.Headers["Authorization"];

            _logger.LogDebug("[{Url}] [{Method}] Authorization: [{Authorization}]",
                requestUrl, requestMethod, requestHeaderAuth);

            _logger.LogTrace("[{Url}] [{Method}] Authorization: [{Authorization}] Body: [Body]",
                requestUrl, requestMethod, requestHeaderAuth, bodyContent);

            await _next.Invoke(context);
        }
    }
}
