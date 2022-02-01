using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class CastInputDto
    {
        [XmlElement("FullName")]
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string FullName { get; set; }
        [XmlElement("IsMainCharacter"), Required]
        public string IsMainCharacter { get; set; }
        [XmlElement("PhoneNumber")]
        [RegularExpression(Constants.PHONE_FORMAT), Required]
        public string PhoneNumber { get; set; }
        [XmlElement("PlayId"), Required]
        public string PlayId { get; set; }
    }
}
