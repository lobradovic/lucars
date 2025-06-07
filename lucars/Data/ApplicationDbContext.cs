using lucars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lucars.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Marka> Markas { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Klasa> Klasas { get; set; }
        public DbSet<Automobil> Automobils { get; set; }
        public DbSet<Zakup> Zakups { get; set; }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model>()
            .HasOne(m => m.Markas)
            .WithMany()
            .HasForeignKey(m => m.idMarka)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Automobil>()
            .HasOne(m => m.Model)
            .WithMany()
            .HasForeignKey(m => m.idModel)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Automobil>()
            .HasOne(k => k.Klasa)
            .WithMany()
            .HasForeignKey(k => k.idKlasa)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Zakup>()
            .HasOne(a => a.Automobil)
            .WithMany()
            .HasForeignKey(a => a.idAutomobil)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Zakup>()
            .HasOne(k => k.User)
            .WithMany()
            .HasForeignKey(k => k.idUser)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}