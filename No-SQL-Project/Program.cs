using System.Text;
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using No_SQL_Project.Models;
using No_SQL_Project.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5001");

string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationRoot config = BuildConfiguration(environmentName);

// Add services to the container.
builder.Services.Configure<MoviesDatabaseSettings>(config.GetSection("MoviesDatabase"));
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IOptions<MoviesDatabaseSettings>>().Value);
builder.Services.AddIdentityMongoDbProvider<User>(
    identityOptions =>
    {
        // Configure identity options here. For example:
        // identityOptions.Password.RequireDigit = true;
    },
    mongoIdentityOptions =>
    {
        mongoIdentityOptions.ConnectionString =
            builder.Configuration.GetSection("MoviesDatabase:ConnectionString").Value;
    });

byte[] secretKey =
    Encoding.ASCII.GetBytes(config["AppSettings:Secret"] ??
                            throw new InvalidOperationException("JWT Secret is not found"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["AppSettings:JWTIssuer"],
        ValidAudience = config["AppSettings:JWTAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NoSQLProject.API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();

static IConfigurationRoot BuildConfiguration(string environmentName)
{
    return new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, false)
        .AddJsonFile($"appsettings.{environmentName}.json", true, false)
        .Build();
}