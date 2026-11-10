using System.ComponentModel.DataAnnotations;

namespace CarDealershipManager.ViewModels.Owner
{
    public class OwnerCreateViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(30, ErrorMessage = "Max 30 symbols")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(30, ErrorMessage = "Max 30 symbols")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please select at least one car dealership")]
        public List<int> CarDealershipIds { get; set; }
    }
}
