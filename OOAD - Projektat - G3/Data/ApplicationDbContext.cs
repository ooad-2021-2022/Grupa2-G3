using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOAD___Projektat___G3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOAD___Projektat___G3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
       options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<KorisnikKompanija> KorisnikKompanija { get; set; }
        public DbSet<RegistrovaniKorisnik> RegistrovaniKorisnik { get; set; }
        public DbSet<NeregistrovaniKorisnik> NeregistrovaniKorisnik { get; set; }
        public DbSet<Artikal> Artikal { get; set; }
        public DbSet<ArtikalKategorija> ArtikalKategorija { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<RegistrovaniKorisnik>().ToTable("RegistrovaniKorisnik");
            modelBuilder.Entity<KorisnikKompanija>().ToTable("KorisnikKompanija");
            modelBuilder.Entity<NeregistrovaniKorisnik>().ToTable("NeregistrovaniKorisnik");
            modelBuilder.Entity<Artikal>().ToTable("Artikal");
            modelBuilder.Entity<ArtikalKategorija>().ToTable("ArtikalKategorija");
            base.OnModelCreating(modelBuilder);
        }
    }
}
