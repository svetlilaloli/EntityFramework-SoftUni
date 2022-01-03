using System.Xml.Serialization;

namespace ProductShop.Dto.Output
{
    [XmlType("User")]
    public class UserWithProductsOutputDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("age")]
        public int? Age { get; set; }
        [XmlElement("SoldProducts")]
        public ProductsCountOutputDto SoldProducts { get; set; }
    }
}
