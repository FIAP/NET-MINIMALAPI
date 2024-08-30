using Microsoft.OpenApi.Models;

namespace dotnet_minimalapi
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Stocks API",
                    Version = "v1",
                    Description = "Descrição da Sua API",
                    Contact = new OpenApiContact
                    {
                        Name = "Thiago S Adriano",
                        Email = "prof.thiagoadriano@teste.com",
                    }
                });
            });
        }
    }
}
