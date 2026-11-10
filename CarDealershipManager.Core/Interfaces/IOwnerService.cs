using CarDealershipData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipManager.Core.Interfaces
{
    public interface IOwnerService
    {
        Task<List<Owner>> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);
        Task<Owner?> GetOwnerWithCarDealershipsAsync(int id);
        Task AddOwnerAsync(Owner owner);
        Task UpdateOwnerAsync(Owner owner, List<int> carDealershipIds);
        Task DeleteOwnerAsync(int id);
    }
}
