using MediatR;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data;
using MedicineManaging.Infrastructure.Data.Config;
using MedicineManaging.Infrastructure.Data.Repositories;
using MedicineManaging.Infrastructure.MediatR.Queries;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
var configuration = new ConfigurationBuilder()
        .SetBasePath(projectPath)
        .AddJsonFile("appsettings.json")
        .Build();

// Add services to the container.

builder.Services.Configure<DatabaseSettings>(configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoContext>();

builder.Services.AddMediatR(typeof(GetMedicinesQuery).GetTypeInfo().Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

var app = builder.Build();

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