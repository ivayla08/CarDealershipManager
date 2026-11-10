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
    public class CarDealershipService : ICarDealershipService
    {
        private readonly CarDbContext context;

        public CarDealershipService(CarDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CarDealership>> GetAllCarDealershipsAsync(string? search = null, int? minCars = null, string? sortOrder = null)
        {
            var query = context.CarDealerships
                .Include(d => d.Cars)
                .Where(d => !d.isDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => d.Name.Contains(search));
            }

            if (minCars.HasValue)
            {
                query = query.Where(d => d.Cars.Count >= minCars.Value);
            }

            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(d => d.Name);
                    break;
                case "cars_desc":
                    query = query.OrderByDescending(d => d.Cars.Count);
                    break;
                case "cars_asc":
                    query = query.OrderBy(d => d.Cars.Count);
                    break;
                default:
                    query = query.OrderBy(d => d.Name);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<CarDealership?> GetCarDealershipDetailsAsync(int id)
        {
            return await context.CarDealerships
                .Include(d => d.Cars)
                .Include(d => d.Owners)
                    .ThenInclude(o => o.Owner)
                .FirstOrDefaultAsync(d => d.Id == id && !d.isDeleted);
        }

        public async Task<CarDealership?> GetCarDealershipByIdAsync(int id)
        {
            return await context.CarDealerships.FindAsync(id);
        }

        public async Task AddCarDealershipAsync(CarDealership carDealership)
        {
            context.CarDealerships.Add(carDealership);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCarDealershipAsync(CarDealership carDealership)
        {
            await context.SaveChangesAsync();
        }

        // soft delete - както при Hospital
        public async Task DeleteCarDealershipAsync(int id)
        {
            var carDealership = await context.CarDealerships.FindAsync(id);

            if (carDealership != null)
            {
                carDealership.isDeleted = true;
                await context.SaveChangesAsync();
            }
        }
    }
}
