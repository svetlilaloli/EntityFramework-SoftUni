using System.Xml.Serialization;

namespace CarDealer.Dto.OutputDto
{
    [XmlType("part")]
    public class PartOutputDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
