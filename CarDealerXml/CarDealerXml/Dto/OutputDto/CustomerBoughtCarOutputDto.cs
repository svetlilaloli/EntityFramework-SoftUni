using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("customer")]
    public class CustomerBoughtCarOutputDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }
        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }
        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
