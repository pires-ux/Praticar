 
using Api01.Data;
using Api01.Repository;
using Api01.Repository.Interface;
using Api01.Service;
using Api01.Middleware;
using Api01.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Banco>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Banco")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();
            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddScoped<JWTservice>();

            builder.Services.AddScoped<IServiceJogadores, ServiceJogadores>();

            builder.Services.AddScoped<IRepositoryJogadores, RepositoryJogadores>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<LogRequisicaoMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
