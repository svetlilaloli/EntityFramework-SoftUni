using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Car
    {
        public Car()
        {
            Sales = new HashSet<Sale>();
            PartCars = new HashSet<PartCar>();
        }
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<PartCar> PartCars { get; set; }
    }
}
