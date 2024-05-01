using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRelations.Models;
using Microsoft.EntityFrameworkCore;

namespace EFRelations
{
    internal class BrickContext: DbContext
    {
        public BrickContext(DbContextOptions<BrickContext> options)
            : base(options) { }
        public DbSet<Brick> Bricks { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<BrickAvailability> BrickAvailabilities { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
            modelBuilder.Entity<MiniFigureHead>().HasBaseType<Brick>();
        }
    }
}
