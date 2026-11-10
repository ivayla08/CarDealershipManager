using System.ComponentModel.DataAnnotations;

namespace CarDealershipManager.ViewModels.CarDealership
{
    public class CarDealershipEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
