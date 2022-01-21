using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Shell
    {
        public Shell()
        {
            Guns = new HashSet<Gun>();
        }
        [Key]
        public int Id { get; set; }
        [Range(2, 1680), Required]
        public double ShellWeight { get; set; }
        [StringLength(30, MinimumLength = 4), Required]
        public string Caliber { get; set; }
        public ICollection<Gun> Guns { get; set; }
    }
}
