using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipData.Entities
{
    public class CarDealershipOwner
    {
        public int CarDealershipId { get; set; }
        public CarDealership CarDealership { get; set; }

        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
