using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ucondo_challenge.business.Enum;

namespace ucondo_challenge.api.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Project ucondo-challenge",
                    Description = "This Api is about ucondo-challenge. ",
                    Contact = new OpenApiContact() { Name = "Amauri Leal", Email = "amauri_leal@yahoo.com.br" }
                });

                // Serializar enums como strings
                c.MapType<AccountType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(AccountType)).Select(name => new OpenApiString(name)).ToList<IOpenApiAny>()
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
