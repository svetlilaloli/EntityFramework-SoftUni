using System.Xml.Serialization;

namespace ProductShop.Dto.Output
{
    [XmlType("User")]
    public class UsersSoldOutputDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlArray("soldProducts")]
        public ProductOutputDto[] SoldProducts { get; set; }
    }
}
