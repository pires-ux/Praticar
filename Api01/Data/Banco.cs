using Microsoft.EntityFrameworkCore.Design;
using Api01.Models;
using Microsoft.EntityFrameworkCore;

namespace Api01.Data
{
    public class Banco:DbContext
    {
        public Banco(DbContextOptions<Banco>options): base(options){}

        public DbSet<Jogadores> jogadores { get; set; }
    }
}
