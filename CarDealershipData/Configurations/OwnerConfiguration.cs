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
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(
                new Owner
                {
                    Id = 1,
                    FirstName = "Georgi",
                    LastName = "Ivanov",
                    Email = "georgiivanov@abv.bg",
                    PhoneNumber = "0888 111 222"
                },
                new Owner
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "Petrova",
                    Email = "mariapetrova@abv.bg",
                    PhoneNumber = "0888 111 223"
                },
                new Owner
                {
                    Id = 3,
                    FirstName = "Nikolay",
                    LastName = "Dimitrov",
                    Email = "nikolaydimitrov@abv.bg",
                    PhoneNumber = "0888 111 224"
                }
            );
        }
    }
}
