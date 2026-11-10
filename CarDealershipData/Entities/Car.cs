using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipData.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand {  get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1950,2026)]
        public int Year {  get; set; }

        [Required]
        [MaxLength(50)]
        public string EngineType {  get; set; }

        public int CarDealershipId { get; set; }    
        public CarDealership CarDealership { get; set; }
    }
}
