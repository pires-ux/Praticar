using Api01;
using Api01.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                 d => d.ServiceType == typeof(DbContextOptions<Banco>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<Banco>(options =>
            {
                options.UseSqlServer(
                    "Server=localhost\\SQLEXPRESS;Database=Api01_teste;Trusted_Connection=True;TrustServerCertificate=True");
            });


        });


        
    }

    public void LimparBanco()
    {
        var scope = Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<Banco>();

        db.Database.Migrate();
        db.jogadores.RemoveRange(db.jogadores);
        db.SaveChanges();
    }
}