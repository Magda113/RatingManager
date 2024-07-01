using System.Text;
using Domain.Models;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repository;
using Persistence.Services;
using Serilog;
using WebApi.Auth;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RatingManager WebApi", Version = "v1" });
                // Konfiguracja Swaggera do używania tokenów Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Proszę wprowadzić token JWT z prefiksem 'Bearer' w polu",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"]
                    };
                });
            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<JwtTokenService>();
            builder.Services.AddSingleton<IDapperContext, DapperContext>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
            builder.Services.AddScoped<SeriLogRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/api/error");
            }
            else
            {

                app.UseExceptionHandler("/api/error");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RatingManager WebApi v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}