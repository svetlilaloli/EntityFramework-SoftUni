using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    public class Visitation
    {
        [Key]
        public int VisitationId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [StringLength(250)]
        public string Comments { get; set; }
        [ForeignKey(nameof(Patient)), Required]
        public int PatientId { get; set;}
        public Patient Patient { get; set; }
        [ForeignKey(nameof(Doctor)), Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
