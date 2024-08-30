using dotnet_minimalapi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

#region [DI]
builder.Services.AddScoped<IStockService, StockService>();
#endregion

var app = builder.Build();


#region [Middleware]
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});

app.UseRequestLogging();
app.UseGlobalExceptionHandling();

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapStockEndpoints();

app.Run();
