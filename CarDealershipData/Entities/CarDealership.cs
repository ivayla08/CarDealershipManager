using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipData.Entities
{
    public class CarDealership
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool isDeleted { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<CarDealershipOwner> Owners { get; set; } = new List<CarDealershipOwner>();
    }
}
