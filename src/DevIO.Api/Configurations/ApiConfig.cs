using Asp.Versioning;
using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning.ApiExplorer;

namespace DevIO.Api.Configurations
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
                .AddMvc()
                .AddApiExplorer(option =>
                {
                    option.GroupNameFormat = "'v'VVV";
                    option.SubstituteApiVersionInUrl = true;
                });
            builder.Services.AddSwaggerConfig();
            var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<MeuDbContext>(options =>
                                    options.UseMySql(mySqlConnection,
                                                        ServerVersion.AutoDetect(mySqlConnection)));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.ResolveDependencies();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());


                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithMethods("GET")
                            .WithOrigins("http://desenvolvedor.io")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                            .AllowAnyHeader());
            });
            return builder;
        }

        public static WebApplication UseApiConfig(this WebApplication app)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfig(apiVersionDescriptionProvider);
            }
            else
            {
                app.UseCors("Production");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
