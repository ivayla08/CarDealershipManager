using CarDealershipData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipData.Configurations
{
    public class CarDealershipConfiguration : IEntityTypeConfiguration<CarDealership>
    {
        public void Configure(EntityTypeBuilder<CarDealership> builder)
        {
            builder.HasData(
                new CarDealership
                {
                    Id = 1,
                    Name = "Auto Center Sofia",
                    Address = "Sofia",
                    PhoneNumber = "02 123 4567",
                    Email = "sofia@autocenter.bg",
                    isDeleted = false
                },
                new CarDealership
                {
                    Id = 2,
                    Name = "Plovdiv Motors",
                    Address = "Plovdiv",
                    PhoneNumber = "032 555 1234",
                    Email = "plovdiv@motors.bg",
                    isDeleted = false
                },
                new CarDealership
                {
                    Id = 3,
                    Name = "Varna Auto",
                    Address = "Varna",
                    PhoneNumber = "052 777 8899",
                    Email = "varna@auto.bg",
                    isDeleted = false
                }
            );
        }
    }
}
