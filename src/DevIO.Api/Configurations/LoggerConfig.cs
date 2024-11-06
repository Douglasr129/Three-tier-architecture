using Elmah.Io.Extensions.Logging;

namespace DevIO.Api.Configurations
{
    public static class LoggerConfig
    {
        public static WebApplicationBuilder AddLoggingConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddElmahIo(o =>
            {
                o.ApiKey = "ea8bf1eceb7b46469798bb199d2b1c6b";
                o.LogId = new Guid("e1ac7069-e1f7-4e46-b2a8-df4b69b0d925");
            });
            //builder.Services.AddLogging(longBuilder =>
            //{
            //    longBuilder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "ea8bf1eceb7b46469798bb199d2b1c6b";
            //        o.LogId = new Guid("e1ac7069-e1f7-4e46-b2a8-df4b69b0d925");
            //    });
            //    longBuilder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});
            return builder;
        }
        public static WebApplication UseLogginConfiguration(this WebApplication app)
        {
            app.UseElmahIo();
            return app;
        }
    }
}
