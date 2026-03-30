using EleicaoBrasilApi.models;
using Microsoft.EntityFrameworkCore;

namespace EleicaoBrasilApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Candidato> Candidatos { get; set; }
    }
}