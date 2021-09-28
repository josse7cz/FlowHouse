using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowHouse.Models;
using Microsoft.EntityFrameworkCore;
//napojeni na db
namespace FlowHouse.Data
{
    public class SkladContext : DbContext

    {
        public SkladContext(DbContextOptions<SkladContext> options) : base(options)
        {
        }
        public DbSet<Polozka> Polozky { get; set; }
        public DbSet<Nakup> Nakupy { get; set; }
        public DbSet<Zakaznik> Zakaznici { get; set; }
        public DbSet<Oddeleni> Oddelenis { get; set; }
        public DbSet<Prodavac> Prodavaci { get; set; }
        public DbSet<Pobocka> Pobocky { get; set; }
        public DbSet<ProdejZadani> ProdejZadanis { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Polozka>().ToTable("Polozka");
            modelBuilder.Entity<Nakup>().ToTable("Nakup");
            modelBuilder.Entity<Zakaznik>().ToTable("Zakaznik");
            modelBuilder.Entity<Oddeleni>().ToTable("Oddělení");
            modelBuilder.Entity<Prodavac>().ToTable("Prodavac");
            modelBuilder.Entity<Pobocka>().ToTable("Pobocka");
            modelBuilder.Entity<ProdejZadani>().ToTable("ProdejZadani");

            modelBuilder.Entity<ProdejZadani>()
                .HasKey(c => new { c.PolozkaID, c.ProdavacID });


        }
    }
}
