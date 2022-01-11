using MediatR;
using MedicineManaging.API.GraphQL.Mutations;
using MedicineManaging.API.GraphQL.Queries;
using MedicineManaging.API.GraphQL.Types;
using MedicineManaging.API.Utilities;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data;
using MedicineManaging.Infrastructure.Data.Config;
using MedicineManaging.Infrastructure.Data.Repositories;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;
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
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();

builder.Services
      .AddGraphQLServer("ClinicSchema")
           .AddQueryType<ClinicQuery>()
           .AddMutationType<ClinicMutation>()
      .AddType<ClinicType>();

builder.Services
      .AddGraphQLServer("MedicineSchema")
           .AddQueryType<MedicineQuery>()
           .AddMutationType<MedicineMutation>()
      .AddType<MedicineType>();

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

app.MapGraphQL("/api/graphql/clinic", "ClinicSchema");
app.MapGraphQL("/api/graphql/medicine", "MedicineSchema");

app.Use(async (context, next) =>
{
    var token = JwtTokenParser.ParseSecurityToken(context);

    bool isValid = token is not null && JwtTokenValidator.ValidateToken(token);

    if (!isValid && !context.Request.Path.Value.Contains("/api/graphql"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
    else await next?.Invoke();
});

app.Run();