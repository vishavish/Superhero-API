using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;
using Superhero.Api.Interfaces;
using Superhero.Api.Repositories;
using Superhero.Api.Middlewares;
using Superhero.Api.Extension;
using Microsoft.AspNetCore.Identity;
using Superhero.Api.Entities.Auth;

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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddValidation();
builder.Services.AddServices();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDb();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
