using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerPrisonerInputDto
    {
        [XmlElement("Name")]
        [MinLength(Constants.OfficerName_Min_Length), MaxLength(Constants.OfficerName_Max_Length), Required]
        public string FullName { get; set; }
        [XmlElement("Money")]
        [Range(Constants.Min_Salary, Constants.Max_Salary), Required]
        public decimal Salary { get; set; }
        [XmlElement("Position"), Required]
        public string Position { get; set; }
        [XmlElement("Weapon"), Required]
        public string Weapon { get; set; }
        [XmlElement("DepartmentId"), Required]
        public int DepartmentId { get; set; }
        [XmlArray("Prisoners")]
        public PrisonerInputDto[] Prisoners { get; set; }
    }
}
