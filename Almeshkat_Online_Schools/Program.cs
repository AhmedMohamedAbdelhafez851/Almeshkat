using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Almeshkat_Online_Schools.Utilities;
using BL.Data;
using Domains;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog (Encapsulated in SerilogConfig)
SerilogConfig.ConfigureLogger();

// Use Serilog for logging
builder.Host.UseSerilog();

// Configure Autofac as the DI container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register Authentication and JWT services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userPrincipal = context.Principal;
            if (userPrincipal != null)
            {
                var identity = (System.Security.Claims.ClaimsIdentity)userPrincipal.Identity!;
                identity.AddClaim(new System.Security.Claims.Claim(
                    System.Security.Claims.ClaimTypes.Name,
                    userPrincipal.FindFirst("name")?.Value ?? ""
                ));
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// Register custom services and interceptors
builder.Services.RegisterCustomServices();

// Add Controllers
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("") // Add specific origins as needed
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add Error Handling Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Ensure that Serilog is closed and flushed at the end of the application’s lifecycle
try
{
    Log.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
