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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Polozka>().ToTable("Polozka");
            modelBuilder.Entity<Nakup>().ToTable("Nakup");
            modelBuilder.Entity<Zakaznik>().ToTable("Zakaznik");
        }
    }
}
