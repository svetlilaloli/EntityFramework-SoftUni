using System.Xml.Serialization;

namespace ProductShop.Dto.Output
{
    [XmlType("SoldProducts")]
    public class ProductsCountOutputDto
    {
        [XmlElement("count")]
        public int Count { get; set; }
        [XmlArray("products")]
        public ProductOutputDto[] Products { get; set; }
    }
}
