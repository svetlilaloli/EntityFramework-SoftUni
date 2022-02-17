using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Department
    {
        public Department()
        {
            Cells = new HashSet<Cell>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.Department_Name_Min_Length), MaxLength(Constants.Department_Name_Max_Length), Required]
        public string Name { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
    }
}