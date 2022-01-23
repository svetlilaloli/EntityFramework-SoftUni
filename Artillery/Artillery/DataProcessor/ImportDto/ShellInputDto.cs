using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ShellInputDto
    {
        [XmlElement("ShellWeight"), Range(Constants.MinShellWeight, Constants.MaxShellWeight), Required]
        public double ShellWeight { get; set; }
        [XmlElement("Caliber"), MinLength(Constants.MinCaliberLength), MaxLength(Constants.MaxCaliberLength), Required]
        public string Caliber { get; set; }
    }
}
