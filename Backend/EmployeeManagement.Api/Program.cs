using AutoMapper;
using EmployeeManagement.Application.Mapping;
using EmployeeManagement.Domain.Repositories;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
