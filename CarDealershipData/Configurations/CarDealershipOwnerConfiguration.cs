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
    public class CarDealershipOwnerConfiguration : IEntityTypeConfiguration<CarDealershipOwner>
    {
        public void Configure(EntityTypeBuilder<CarDealershipOwner> builder)
        {
            builder.HasData(
                new CarDealershipOwner { CarDealershipId = 1, OwnerId = 1 },
                new CarDealershipOwner { CarDealershipId = 1, OwnerId = 2 },
                new CarDealershipOwner { CarDealershipId = 2, OwnerId = 2 },
                new CarDealershipOwner { CarDealershipId = 3, OwnerId = 3 }
            );
        }
    }
}
