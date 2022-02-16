using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManaging.API.Services.Users;
using UserManaging.API.Utilities;
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
builder.Services.AddSwaggerGen(c =>
{
    // don't added to Authorization Header
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into this field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
        },
        new string[] {}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("UserManagingDatabase"));
});

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.Audience = "API";
                     options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                 });

builder.Services.AddAuthorization();

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

builder.Services.AddControllers();

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

app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    var token = HttpUserHelper.ParseSecurityToken(context);

    if (token is null)
    {
        var user = context.User.Claims.FirstOrDefault(claim => claim.Type.Equals("sub"))?.Value;

        if (user is null)
        {
            var webPath = context.Request.Path.Value.ToLower();

            if (!(webPath.Contains("login") || webPath.Contains("register") || webPath.Equals("/swagger/index.html")))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
    }

    await next();
});

app.Run();
