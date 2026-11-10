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
    public class OwnerService : IOwnerService
    {
        private readonly CarDbContext context;

        public OwnerService(CarDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Owner>> GetAllOwnersAsync()
        {
            return await context.Owners
                .Include(o => o.CarDealerships)
                    .ThenInclude(cd => cd.CarDealership)
                .ToListAsync();
        }

        public async Task<Owner?> GetOwnerByIdAsync(int id)
        {
            return await context.Owners.FindAsync(id);
        }

        // Details, Edit GET
        public async Task<Owner?> GetOwnerWithCarDealershipsAsync(int id)
        {
            return await context.Owners
                .Include(o => o.CarDealerships)
                    .ThenInclude(cd => cd.CarDealership)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Create POST (връзките към автокъщите идват вече добавени в owner.CarDealerships)
        public async Task AddOwnerAsync(Owner owner)
        {
            context.Owners.Add(owner);
            await context.SaveChangesAsync();
        }

        // Edit POST - синхронизира връзките в междинната таблица с избраните автокъщи
        public async Task UpdateOwnerAsync(Owner owner, List<int> carDealershipIds)
        {
            var current = await context.CarDealershipOwners
                .Where(x => x.OwnerId == owner.Id)
                .ToListAsync();

            var selectedIds = carDealershipIds.Distinct().ToList();

            context.CarDealershipOwners.RemoveRange(
                current.Where(x => !selectedIds.Contains(x.CarDealershipId)));

            var existingIds = current.Select(x => x.CarDealershipId).ToHashSet();

            foreach (var id in selectedIds.Where(id => !existingIds.Contains(id)))
            {
                context.CarDealershipOwners.Add(new CarDealershipOwner
                {
                    OwnerId = owner.Id,
                    CarDealershipId = id
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteOwnerAsync(int id)
        {
            var owner = await context.Owners
                .Include(o => o.CarDealerships)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner != null)
            {
                context.CarDealershipOwners.RemoveRange(owner.CarDealerships);
                context.Owners.Remove(owner);
                await context.SaveChangesAsync();
            }
        }
    }
}
