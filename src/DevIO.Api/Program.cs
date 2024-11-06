using DevIO.Api.Configurations;
var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfig();
builder.AddIdentityConfiguration();
builder.AddLoggingConfiguration();
var app = builder.Build();
app.UseApiConfig();
app.UseLogginConfiguration(); 


app.Run();
