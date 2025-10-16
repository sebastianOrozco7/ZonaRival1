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

            // Relación: un Partido tiene un Equipo Retador
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.EquipoRetador)
                .WithMany(e => e.PartidosComoRetador)
                .HasForeignKey(p => p.EquipoRetadorId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Relación: un Partido tiene un Equipo Desafiado
            modelBuilder.Entity<Partido>()
                .HasOne(p => p.EquipoDesafiado)
                .WithMany(e => e.PartidosComoDesafiado)
                .HasForeignKey(p => p.EquipoDesafiadoId)
                .OnDelete(DeleteBehavior.Restrict); 

            //es importante, porque si un equipo se elimina, no quieres que todos los partidos donde participó también se borren automáticamente.
        }
    }
}
