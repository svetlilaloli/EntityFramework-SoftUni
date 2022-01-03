using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            Visitations = new HashSet<Visitation>();
            Diagnoses = new HashSet<Diagnose>();
            PatientMedicaments = new HashSet<PatientMedicament>();
        }
        [Key]
        public int PatientId { get; set; }
        [StringLength(50), Required]
        public string FirstName { get; set; }
        [StringLength(50), Required]
        public string LastName { get; set; }
        [StringLength(250), Required]
        public string Address { get; set; }
        [StringLength(80)]
        public string Email { get; set; }
        [Required]
        public bool HasInsurance { get; set; }
        public ICollection<Visitation> Visitations { get; set; }
        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<PatientMedicament> PatientMedicaments { get; set; }

    }
}
