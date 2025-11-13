using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    public class ScreenSoundContext : DbContext   
    {
        string connectionString = "Data Source=DESKTOP-5A72BGU\\SQLEXPRESS;Initial Catalog= ScreenSound;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>()
                .HasMany(m => m.Generos)
                .WithMany(g => g.Musicas);
        }

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}
