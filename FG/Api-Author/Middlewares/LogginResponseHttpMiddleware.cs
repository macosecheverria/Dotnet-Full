namespace Api_Author.Middleware;


public static class LogginResponseHttpMiddlewareExtension
{

    public static IApplicationBuilder UseLogginResponseHttp(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogginResponseHttpMiddleware>();
    }
}


public class LogginResponseHttpMiddleware
{

    private readonly RequestDelegate next;

    private readonly ILogger<LogginResponseHttpMiddleware> logger;

    public LogginResponseHttpMiddleware(
        RequestDelegate next,
        ILogger<LogginResponseHttpMiddleware> logger
        )
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var ms = new MemoryStream())
        {
            var responseBodyOriginal = context.Response.Body;

            context.Response.Body = ms;
            await next(context);

            ms.Seek(0, SeekOrigin.Begin);
            string response = new StreamReader(ms).ReadToEnd();
            ms.Seek(0, SeekOrigin.Begin);
            await ms.CopyToAsync(responseBodyOriginal);
            context.Response.Body = responseBodyOriginal;

            logger.LogInformation(response);
        }
    }
}