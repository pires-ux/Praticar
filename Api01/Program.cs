 
using Api01.Data;
using Api01.Repository;
using Api01.Repository.Interface;
using Api01.Service;
using Api01.Middleware;
using Api01.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Banco>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Banco")));
            
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IServiceJogadores, ServiceJogadores>();

            builder.Services.AddScoped<IRepositoryJogadores, RepositoryJogadores>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<LogRequisicaoMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
