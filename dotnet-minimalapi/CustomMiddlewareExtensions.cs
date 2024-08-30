namespace dotnet_minimalapi;

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            await next.Invoke();
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        });
    }

    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            }
        });
    }
}
