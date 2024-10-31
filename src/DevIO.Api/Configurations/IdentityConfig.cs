using DevIO.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Api.Configurations
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                    options.UseMySql(mySqlConnection,
                                                        ServerVersion.AutoDetect(mySqlConnection)));
            return builder;
        }
    }
}
