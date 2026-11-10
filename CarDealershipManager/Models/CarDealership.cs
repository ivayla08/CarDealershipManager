using System.ComponentModel.DataAnnotations;

namespace CarDealershipManager.Models
{
    public class CarDealership
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50,ErrorMessage ="Max 50 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 symbols")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Name is required")]      
        [Phone(ErrorMessage ="Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

    }
}
