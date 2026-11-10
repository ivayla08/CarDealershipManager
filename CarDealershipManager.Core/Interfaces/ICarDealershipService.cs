using CarDealershipData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipManager.Core.Interfaces
{
    public interface ICarDealershipService
    {
        Task<List<CarDealership>> GetAllCarDealershipsAsync(string? search = null, int? minCars = null, string? sortOrder = null);
        Task<CarDealership?> GetCarDealershipDetailsAsync(int id);
        Task<CarDealership?> GetCarDealershipByIdAsync(int id);
        Task AddCarDealershipAsync(CarDealership carDealership);
        Task UpdateCarDealershipAsync(CarDealership carDealership);
        Task DeleteCarDealershipAsync(int id);
    }
}
