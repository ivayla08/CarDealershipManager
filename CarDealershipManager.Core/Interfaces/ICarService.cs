using CarDealershipData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipManager.Core.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car?> GetCarDetailsAsync(int id);
        Task<Car?> GetCarByIdAsync(int id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}
