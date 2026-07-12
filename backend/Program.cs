using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using backend.Models.User;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];
var key = jwtKey != null ? Encoding.UTF8.GetBytes(jwtKey) : throw new InvalidOperationException("JWT key not found in configuration.");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<
    IPasswordHasher<UserModel>,
    PasswordHasher<UserModel>>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAssertion(_ => true)
            .Build();
    });
}
else
{
    builder.Services.AddAuthorization();
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite's default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Logger.LogWarning("Authorization bypass is ENABLED (Development environment).");
}
else
{
    app.Logger.LogInformation("Authorization bypass is disabled (non-Development environment).");
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("OpenPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

