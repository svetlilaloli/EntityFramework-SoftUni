using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Manufacturer")]
    public class ManufacturerInputDto
    {
        [XmlElement("ManufacturerName"), MinLength(Constants.MinManufacturerNameLength), MaxLength(Constants.MaxManufacturerNameLength), Required]
        public string ManufacturerName { get; set; }
        [XmlElement("Founded"), MinLength(Constants.MinFoundedLength), MaxLength(Constants.MaxFoundedLength), Required]
        public string Founded { get; set; }
    }
}
