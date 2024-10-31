using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Api.Configurations
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfig(this  WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.ResolveDependencies();
            return builder;
        }

        public static WebApplication UseApiConfig(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            return app;
        }
    }
}
