using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<VehicleAccessory> VehicleAccessories { get; set; }
        public DbSet<User> Users { get; set; }

        // for many to many relationship between vehicles and its accessories
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // first set the composite key
            modelBuilder.Entity<VehicleAccessory>()
                .HasKey(va => new { va.VehicleId, va.AccessoryId });

            // 1 vehicle can have many accessories
            modelBuilder.Entity<VehicleAccessory>()
                .HasOne(va => va.Vehicle)
                .WithMany(va => va.Accessories)
                .HasForeignKey(va => va.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 accessory can be in multiple vehicles
            modelBuilder.Entity<VehicleAccessory>()
                .HasOne(va => va.Accessory)
                .WithMany(va => va.Vehicles)
                .HasForeignKey(va => va.AccessoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
