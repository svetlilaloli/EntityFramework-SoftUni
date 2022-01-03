using System.Xml.Serialization;

namespace CarDealer.Dto.InputDto
{
    [XmlType("Sale")]
    public class SaleInputDto
    {
        [XmlElement("carId")]
        public int CarId { get; set; }
        [XmlElement("customerId")]
        public int CustomerId { get; set; }
        [XmlElement("discount")]
        public int Discount { get; set; }
    }
}
