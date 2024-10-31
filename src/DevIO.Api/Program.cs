using DevIO.Api.Configurations;
var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfig();
builder.AddIdentityConfiguration();
var app = builder.Build();

app.UseApiConfig();

app.Run();
