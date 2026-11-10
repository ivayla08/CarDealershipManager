using CarDealershipData.Entities;
using CarDealershipManager.Core.Interfaces;
using CarDealershipManager.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealershipManager.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly ICarDealershipService carDealershipService;

        public CarController(ICarService carService, ICarDealershipService carDealershipService)
        {
            this.carService = carService;
            this.carDealershipService = carDealershipService;
        }

        private async Task LoadCarDealerships()
        {
            ViewBag.CarDealerships = new SelectList(
                await carDealershipService.GetAllCarDealershipsAsync(),
                "Id",
                "Name");
        }

        public async Task<IActionResult> Index()
        {
            var cars = await carService.GetAllCarsAsync();

            var model = new List<CarIndexViewModel>();

            foreach (var car in cars)
            {
                model.Add(new CarIndexViewModel
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    EngineType = car.EngineType,
                    CarDealershipName = car.CarDealership.Name
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await carService.GetCarDetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var model = new CarDetailsViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EngineType = car.EngineType,
                CarDealershipName = car.CarDealership.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCarDealerships();

            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    Year = viewModel.Year,
                    EngineType = viewModel.EngineType,
                    CarDealershipId = viewModel.CarDealershipId
                };

                await carService.AddCarAsync(car);

                return RedirectToAction(nameof(Index));
            }

            await LoadCarDealerships();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var model = new CarEditViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EngineType = car.EngineType,
                CarDealershipId = car.CarDealershipId
            };

            await LoadCarDealerships();

            return View(model);
        }

      
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var car = await carService.GetCarByIdAsync(id);

                if (car == null)
                {
                    return NotFound();
                }

                car.Brand = viewModel.Brand;
                car.Model = viewModel.Model;
                car.Year = viewModel.Year;
                car.EngineType = viewModel.EngineType;
                car.CarDealershipId = viewModel.CarDealershipId;

                await carService.UpdateCarAsync(car);

                return RedirectToAction(nameof(Index));
            }

            await LoadCarDealerships();

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var model = new CarDeleteViewModel
            {
                Id = car.Id,
                Model = car.Model,
                Brand = car.Brand,
                Year = car.Year,
                EngineType = car.EngineType
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await carService.GetCarByIdAsync(id);

            if (car != null)
            {
                await carService.DeleteCarAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
