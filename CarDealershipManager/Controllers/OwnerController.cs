using CarDealershipData.Entities;
using CarDealershipManager.Core.Interfaces;
using CarDealershipManager.ViewModels.Owner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealershipManager.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerService ownerService;
        private readonly ICarDealershipService carDealershipService;

        public OwnerController(IOwnerService ownerService, ICarDealershipService carDealershipService)
        {
            this.ownerService = ownerService;
            this.carDealershipService = carDealershipService;
        }

        private async Task LoadCarDealerships(List<int>? selectedCarDealerships = null)
        {
            var carDealerships = await carDealershipService.GetAllCarDealershipsAsync();

            ViewBag.CarDealerships = new MultiSelectList(
                carDealerships,
                "Id",
                "Name",
                selectedCarDealerships);
        }

        public async Task<IActionResult> Index()
        {
            var owners = await ownerService.GetAllOwnersAsync();

            var model = new List<OwnerIndexViewModel>();

            foreach (var item in owners)
            {
                model.Add(new OwnerIndexViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    CarDealershipNames = string.Join(", ", item.CarDealerships.Select(cd => cd.CarDealership.Name))
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var owner = await ownerService.GetOwnerWithCarDealershipsAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            var model = new OwnerDetailsViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber,
                CarDealershipNames = string.Join(", ", owner.CarDealerships.Select(cd => cd.CarDealership.Name))
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCarDealerships();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OwnerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = new Owner
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                foreach (var carDealershipId in model.CarDealershipIds.Distinct())
                {
                    // Id-то на собственика се попълва автоматично при запис
                    owner.CarDealerships.Add(new CarDealershipOwner
                    {
                        CarDealershipId = carDealershipId
                    });
                }

                await ownerService.AddOwnerAsync(owner);

                return RedirectToAction(nameof(Index));
            }

            await LoadCarDealerships(model.CarDealershipIds);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var owner = await ownerService.GetOwnerWithCarDealershipsAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            var model = new OwnerEditViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber,
                CarDealershipIds = owner.CarDealerships.Select(cd => cd.CarDealershipId).ToList()
            };

            await LoadCarDealerships(model.CarDealershipIds);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OwnerEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var owner = await ownerService.GetOwnerByIdAsync(id);

                if (owner == null)
                {
                    return NotFound();
                }

                owner.FirstName = model.FirstName;
                owner.LastName = model.LastName;
                owner.Email = model.Email;
                owner.PhoneNumber = model.PhoneNumber;

                await ownerService.UpdateOwnerAsync(owner, model.CarDealershipIds);

                return RedirectToAction(nameof(Index));
            }

            await LoadCarDealerships(model.CarDealershipIds);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var owner = await ownerService.GetOwnerByIdAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            var model = new OwnerDeleteViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await ownerService.GetOwnerByIdAsync(id);

            if (owner != null)
            {
                await ownerService.DeleteOwnerAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
