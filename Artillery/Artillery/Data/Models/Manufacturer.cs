using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Guns = new HashSet<Gun>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(40, MinimumLength = 4), Required]
        public string ManufacturerName { get; set; }
        [StringLength(100, MinimumLength = 10), Required]
        public string Founded { get; set;}
        public ICollection<Gun> Guns { get; set; }
    }
}
