using Microsoft.EntityFrameworkCore;
using ZonaRival.Models;

namespace ZonaRival.Data
{
    public class ZonaRivalContext : DbContext
    {
        public ZonaRivalContext(DbContextOptions<ZonaRivalContext> options) : base(options){}

        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<Cancha> Canchas {  get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Historial> Historiales {  get; set; }

        //tablas relacionales
        public DbSet<EquipoCancha> EquiposCanchas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definiendo la clave primaria
            modelBuilder.Entity<EquipoCancha>()
                .HasKey(ec => new { ec.EquipoId, ec.CanchaId });

            // Relacionando EquipoCancha con Equipo y con Cancha
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

            //relacionando partido con equipo (2 equipos por partido)
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.equipo1)
                .WithMany()
                .HasForeignKey(p => p.EquipoId1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partido>()
                .HasOne(p => p.equipo2)
                .WithMany()
                .HasForeignKey(p => p.EquipoId2)
                .OnDelete(DeleteBehavior.Restrict);

            //Relacion de historial con partido
            modelBuilder.Entity<Historial>()
                .HasOne(h => h.partido)
                .WithMany(p => p.historiales)
                .HasForeignKey(h => h.PartidoId);

            //Relacon de historial con equipo
            modelBuilder.Entity<Historial>()
                .HasOne(h => h.equipo)
                .WithMany(e => e.historiales)
                .HasForeignKey(h => h.EquipoId);
        }
    }
}
