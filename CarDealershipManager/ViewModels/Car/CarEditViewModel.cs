using System.ComponentModel.DataAnnotations;

namespace CarDealershipManager.ViewModels.Car
{
    public class CarEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1950, 2026)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Engine type is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string EngineType { get; set; }

        [Required(ErrorMessage = "Choose a dealership")]
        public int CarDealershipId { get; set; }

        }
}
