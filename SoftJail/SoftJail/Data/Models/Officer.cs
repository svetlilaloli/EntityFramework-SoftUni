using SoftJail.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Officer
    {
        public Officer()
        {
            OfficerPrisoners = new HashSet<OfficerPrisoner>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.OfficerName_Min_Length), MaxLength(Constants.OfficerName_Max_Length), Required]
        public string FullName { get; set; }
        [Range(Constants.Min_Salary, Constants.Max_Salary), Required]
        public decimal Salary { get; set; }
        [Required]
        public Position Position { get; set; }
        [Required]
        public Weapon Weapon { get; set; }
        [ForeignKey(nameof(Department)), Required]
        public int DepartmentId { get; set; }
        [Required]
        public virtual Department Department { get; set; }
        public virtual ICollection<OfficerPrisoner> OfficerPrisoners { get; set; }
    }
}
