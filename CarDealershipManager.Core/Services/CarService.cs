using CarDealershipData;
using CarDealershipData.Entities;
using CarDealershipManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipManager.Core.Services
{
    public class CarService : ICarService
    {
        private readonly CarDbContext context;

        public CarService(CarDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await context.Cars
                .Include(c => c.CarDealership)
                .ToListAsync();
        }

        public async Task<Car?> GetCarDetailsAsync(int id)
        {
            return await context.Cars
                .Include(c => c.CarDealership)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await context.Cars.FindAsync(id);
        }

        public async Task AddCarAsync(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await context.Cars.FindAsync(id);

            if (car != null)
            {
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
            }
        }
    }
}
