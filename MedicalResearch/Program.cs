using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManaging.API.Services.Users;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;
using UserManaging.Infrastructure.Configuration;
using UserManaging.Infrastructure.Data;
using UserManaging.Infrastructure.Data.EntitiesConfig;
using UserManaging.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
var configuration = new ConfigurationBuilder()
        .SetBasePath(projectPath)
        .AddJsonFile("appsettings.json")
        .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("UserManagingDatabase"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer(options =>
{
    options.UserInteraction.LoginUrl = "/authorization/login";
    options.UserInteraction.LogoutUrl = "/authorization/logout";
})
    .AddProfileService<IdentityServerProfileService>()
    .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
    .AddInMemoryClients(IdentityServerConfiguration.GetClients())
    .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
    .AddInMemoryApiScopes(IdentityServerConfiguration.GetScopes())
    .AddDeveloperSigningCredential();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAuthenticaionService, UserAuthenticationService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<EntityToDTOMappingConfig>());

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
