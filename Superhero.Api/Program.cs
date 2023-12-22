using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;
using Superhero.Api.Interfaces;
using Superhero.Api.Repositories;
using FluentValidation;
using System.Reflection;
using Superhero.Api.Filters;
using Superhero.Api.Middlewares;
using System.Net.Sockets;
using FluentValidation.AspNetCore;
using Superhero.Api.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddValidation();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
