using CarDealershipData.Configurations;
using CarDealershipData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipData
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarDealership> CarDealerships { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<CarDealershipOwner> CarDealershipOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // many-to-many: CarDealership <-> Owner (чрез CarDealershipOwner)
            modelBuilder.Entity<CarDealershipOwner>()
                .HasKey(cdo => new { cdo.CarDealershipId, cdo.OwnerId });

            modelBuilder.Entity<CarDealershipOwner>()
                .HasOne(cdo => cdo.CarDealership)
                .WithMany(cd => cd.Owners)
                .HasForeignKey(cdo => cdo.CarDealershipId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarDealershipOwner>()
                .HasOne(cdo => cdo.Owner)
                .WithMany(o => o.CarDealerships)
                .HasForeignKey(cdo => cdo.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new CarDealershipConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new CarDealershipOwnerConfiguration());
        }
    }
}
