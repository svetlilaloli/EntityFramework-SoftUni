using System.Xml.Serialization;

namespace ProductShop.Dto.Output
{
    [XmlType("Users")]
    public class AllUsersSoldOutputDto
    {
        [XmlElement("count")]
        public int UsersCount { get; set; }
        [XmlArray("users")]
        public UserWithProductsOutputDto[] Users { get; set; }
    }
}
