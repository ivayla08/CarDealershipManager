using CarDealershipData.Entities;
using CarDealershipManager.Core.Interfaces;
using CarDealershipManager.ViewModels.CarDealership;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipManager.Controllers
{
    public class CarDealershipController : Controller
    {
        private readonly ICarDealershipService carDealershipService;

        public CarDealershipController(ICarDealershipService carDealershipService)
        {
            this.carDealershipService = carDealershipService;
        }

        public async Task<IActionResult> Index(string? search, int? minCars, string? sortOrder)
        {
            var carDealerships = await carDealershipService.GetAllCarDealershipsAsync(search, minCars, sortOrder);

            var model = new List<CarDealershipIndexViewModel>();

            foreach (var carD in carDealerships)
            {
                model.Add(new CarDealershipIndexViewModel
                {
                    Id = carD.Id,
                    Name = carD.Name,
                    Address = carD.Address,
                    PhoneNumber = carD.PhoneNumber,
                    Email = carD.Email,
                    CarsCount = carD.Cars.Count
                });
            }

            ViewBag.Search = search;
            ViewBag.MinCars = minCars;
            ViewBag.SortOrder = sortOrder;

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var carDealership = await carDealershipService.GetCarDealershipDetailsAsync(id);

            if (carDealership == null)
            {
                return NotFound();
            }

            var model = new CarDealershipDetailsViewModel
            {
                Id = carDealership.Id,
                Name = carDealership.Name,
                Address = carDealership.Address,
                Email = carDealership.Email,
                PhoneNumber = carDealership.PhoneNumber,
                Cars = carDealership.Cars
                    .Select(c => c.Brand + " " + c.Model + " (" + c.Year + ")")
                    .ToList(),
                Owners = carDealership.Owners
                    .Select(o => o.Owner.FirstName + " " + o.Owner.LastName)
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarDealershipCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carD = new CarDealership
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                await carDealershipService.AddCarDealershipAsync(carD);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var carD = await carDealershipService.GetCarDealershipByIdAsync(id);

            if (carD == null)
            {
                return NotFound();
            }

            var model = new CarDealershipEditViewModel
            {
                Id = carD.Id,
                Name = carD.Name,
                Address = carD.Address,
                Email = carD.Email,
                PhoneNumber = carD.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarDealershipEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var carD = await carDealershipService.GetCarDealershipByIdAsync(id);

                if (carD == null)
                {
                    return NotFound();
                }

                carD.Name = model.Name;
                carD.Address = model.Address;
                carD.Email = model.Email;
                carD.PhoneNumber = model.PhoneNumber;

                await carDealershipService.UpdateCarDealershipAsync(carD);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var carD = await carDealershipService.GetCarDealershipByIdAsync(id);

            if (carD == null)
            {
                return NotFound();
            }

            var model = new CarDealershipDeleteViewModel
            {
                Id = carD.Id,
                Name = carD.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await carDealershipService.DeleteCarDealershipAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
