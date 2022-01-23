using Artillery.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artillery.Data.Models
{
    public class Gun
    {
        public Gun()
        {
            CountriesGuns = new HashSet<CountryGun>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Manufacturer)), Required]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        [Range(100, 1350000), Required]
        public int GunWeight { get; set; }
        [Range(2.00, 35.00), Required]
        public double BarrelLength { get; set; }
        public int? NumberBuild { get; set; }
        [Range(1, 100000), Required]
        public int Range { get; set; }
        [Required]
        public GunType GunType { get; set; }
        [ForeignKey(nameof(Shell)), Required]
        public int ShellId { get; set; }
        public Shell Shell { get; set; }
        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}