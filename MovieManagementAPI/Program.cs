using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.Data;
using MovieManagementAPI.Middlewares;
using MovieManagementAPI.Repositories.Implementations;
using MovieManagementAPI.Repositories.Interfaces;
using MovieManagementAPI.Services.Implementations;
using MovieManagementAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For to return ModelState errors as CustomResult
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// Repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Services
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Global Exception Handling
app.UseMiddleware<GlobalErrorHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
