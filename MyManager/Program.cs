using MyManager.Http;
using MyManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.RegisterModuleServices();

builder.Services.RegisterAutoServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseModuleMiddleware();

app.MapModuleEndpoints();

app.Run();
