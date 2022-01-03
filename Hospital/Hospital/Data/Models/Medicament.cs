using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        public Medicament()
        {
            PatientMedicaments = new HashSet<PatientMedicament>();
        }
        [Key]
        public int MedicamentId { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        public ICollection<PatientMedicament> PatientMedicaments { get; set; }
    }
}
