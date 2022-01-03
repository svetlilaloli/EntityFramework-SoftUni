using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("sale")]
    public class SalesWithDiscountOutputDto
    {
        [XmlElement("car")]
        public CarAttrOutputDto Car { get; set; }
        [XmlElement("discount")]
        public string Discount { get; set; }
        [XmlElement("customer-name")]
        public string CustomerName { get; set; }
        [XmlElement("price")]
        public string Price { get; set; }
        [XmlElement("price-with-discount")]
        public string PriceWithDiscount { get; set; }
    }
}
