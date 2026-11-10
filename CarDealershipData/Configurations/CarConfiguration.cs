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
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
                new Car
                {
                    Id = 1,
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    EngineType = "Hybrid",
                    CarDealershipId = 1
                },
                new Car
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2019,
                    EngineType = "Diesel",
                    CarDealershipId = 2
                },
                new Car
                {
                    Id = 3,
                    Brand = "Volkswagen",
                    Model = "Golf",
                    Year = 2021,
                    EngineType = "Petrol",
                    CarDealershipId = 3
                },
                new Car
                {
                    Id = 4,
                    Brand = "Tesla",
                    Model = "Model 3",
                    Year = 2023,
                    EngineType = "Electric",
                    CarDealershipId = 1
                }
            );
        }
    }
}
