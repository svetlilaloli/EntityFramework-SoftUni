using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Cell
    {
        public Cell()
        {
            Prisoners = new HashSet<Prisoner>();
        }
        [Key]
        public int Id { get; set; }
        [Range(Constants.Cell_Min_Number, Constants.Cell_Max_Number), Required]
        public int CellNumber { get; set; }
        [Required]
        public bool HasWindow { get; set; }
        [ForeignKey(nameof(Department)), Required]
        public int DepartmentId { get; set; }
        [Required]
        public virtual Department Department { get; set; }
        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }
}