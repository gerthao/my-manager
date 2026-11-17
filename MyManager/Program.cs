using System.Diagnostics;
using MyManager.Http;
using MyManager.Routing;
using MyManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.RegisterModuleServices();

builder.Services.RegisterAutoServices();

builder.Services.AddProblemDetails(opts =>
{
    opts.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
    };
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ViteDevPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Location");
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseCors("ViteDevPolicy");
    app.MapOpenApi("openapi/v1.yaml");
}

app.UseHttpsRedirection();

app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseModuleMiddleware();
app.MapModuleEndpoints();
app.RegisterRoutes();

app.Run();
