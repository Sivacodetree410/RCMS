using Microsoft.Extensions.DependencyInjection;
using RCMS.Shared;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add CORS   to connect between angular and core web api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register services to the container.
builder.Services.AddProjectServices();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Keep property names as written (StatusCode, Status, Message, Data)
    options.JsonSerializerOptions.PropertyNamingPolicy = null;

    // Ignore null values
    options.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

// Use CORS
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
