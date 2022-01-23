using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            CountriesGuns = new HashSet<CountryGun>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MinCountryNameLength), MaxLength(Constants.MaxCountryNameLength), Required]
        public string CountryName { get; set; }
        [Range(Constants.MinArmySize, Constants.MaxArmySize), Required]
        public int ArmySize { get; set; }
        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
