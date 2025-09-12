using Microsoft.EntityFrameworkCore;
using ZonaRival.Models;
using ZonaRival.Models.ViewModels;

namespace ZonaRival.Data
{
    public class ZonaRivalContext : DbContext
    {
        public ZonaRivalContext(DbContextOptions<ZonaRivalContext> options) : base(options){}

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<Cancha> Canchas {  get; set; }
        public DbSet<Partido> Partidos { get; set; }
      

        //tablas relacionales
        public DbSet<EquipoCancha> EquiposCanchas { get; set; }
        public DbSet<EquipoPartido> EquiposPartidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionando EquipoCancha con Equipo y con Cancha
            modelBuilder.Entity<EquipoCancha>()
                .HasKey(ec => new { ec.EquipoId, ec.CanchaId });
            modelBuilder.Entity<EquipoCancha>()
                .HasOne(ec => ec.Equipo)
                .WithMany(e => e.EquiposCanchas)
                .HasForeignKey(ec => ec.EquipoId);

            modelBuilder.Entity<EquipoCancha>()
                .HasOne(ec => ec.Cancha)
                .WithMany(c => c.EquiposCanchas)
                .HasForeignKey(ec => ec.CanchaId);

            //relacion de Usuario y Equipo
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Equipo)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.IdEquipo);

            //Relacion de EquipoPartido con Equipo y Partido
            modelBuilder.Entity<EquipoPartido>()
                .HasKey(ep => new { ep.EquipoId, ep.PartidoId });

            modelBuilder.Entity<EquipoPartido>()
                .HasOne(ep => ep.Equipo)
                .WithMany(e => e.equipoPartidos)
                .HasForeignKey(ep => ep.EquipoId);

            modelBuilder.Entity<EquipoPartido>()
                .HasOne(ep => ep.Partido)
                .WithMany(p => p.equipoPartidos)
                .HasForeignKey(ep =>  ep.PartidoId);

            
        }
    }
}
