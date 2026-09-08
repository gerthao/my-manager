using MyManager.Http;
using MyManager.Routing;
using MyManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.RegisterModuleServices();

builder.Services.RegisterAutoServices();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseModuleMiddleware();
app.MapModuleEndpoints();
app.RegisterRoutes();

app.Run();
