using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Visitations = new HashSet<Visitation>();
        }
        [Key]
        public int DoctorId { get; set; }
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(100), Required]
        public string Speciality { get; set; }
        public ICollection<Visitation> Visitations { get; set; }
    }
}
