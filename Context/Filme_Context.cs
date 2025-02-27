using api_filmes_senai.Domains;
using Microsoft.EntityFrameworkCore;

namespace api_filmes_senai.Context
{
    public class Filme_Context : DbContext
    {
        public Filme_Context() { } 
        public Filme_Context(DbContextOptions<Filme_Context> options) : base(options) 
        { }

        public DbSet <Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = DESKTOP-S3CGL03\\SQLEXPRESS; Database=filmes; User Id = sa; Pwd = Senai@134; TrustServerCertificate=true; ");
            }
            
        }
    }
}
